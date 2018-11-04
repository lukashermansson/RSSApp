using RSSApp.BLL;
using RSSApp.Exeptions;
using RSSApp.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace RSSApp.PL {
    public partial class PodcastPlayerMainForm : Form {




        public PodcastPlayerMainForm() {



            FeedsController.UpdatedFeed += UpdatedFeed;

            InitializeComponent();



            loadPersistance();


            FeedsController.UpdatedFeed += UpdatedFeed;

        }

        private void UpdatedFeed(Object sender, EventArgs e) {
            UpdateFeedList();
            
        }

        private void loadPersistance() {
            PersitanceController controller = new PersitanceController();
            var file = controller.Read();

            foreach (var category in file.Categories) {
                CategoriesController.AddCategory(category);
            }
            foreach (var feed in file.feeds)
            {
                feed.InitializeCategory();
                FeedsController.AddFeedAsync(feed);
            }


            UpdateCategories();
            UpdateFeedList();
            
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {

        }

        private void UpdateFeedList() {
            gvFeeds.Rows.Clear();
            foreach (var Feed in FeedsController.GetFeeds()) {
                int row = gvFeeds.Rows.Add();
                gvFeeds.Rows[row].Cells["ColName"].Value = Feed.Title;
                gvFeeds.Rows[row].Cells["ColTimer"].Value = Feed.getUpdateInterval().ToString();

                if (Feed.Podcasts != null)
                {
                    gvFeeds.Rows[row].Cells["ColNumEpisodes"].Value = Feed.Podcasts.Count;
                }
                else {
                    gvFeeds.Rows[row].Cells["ColNumEpisodes"].Value = 0;
                }
                gvFeeds.Rows[row].Cells["ColCategory"].Value = Feed.Category;
                //gvFeeds.Rows[row].Cells["ColCategory"].Value = Feed.Category;
                gvFeeds.Rows[row].Tag = Feed;
            }
        }

        private void UpdateCategoriesList() {
            lvCategories.Items.Clear();
            
            foreach (var category in CategoriesController.GetCategories()) {
                ListViewItem item = new ListViewItem();
                item.Text = category.Name;
                item.Tag = category;
                lvCategories.Items.Add(item);


            }
        }

        private void UpdateCategories() {
            UpdateCategoriesComboBox();
            UpdateCategoriesList();
        }
        
        private void UpdateCategoriesComboBox() {
            cbFeedCategory.Items.Clear();
            ((DataGridViewComboBoxColumn)gvFeeds.Columns["ColCategory"]).Items.Clear();
            foreach (var item in CategoriesController.GetCategories()) {
                cbFeedCategory.Items.Add(item);
                ((DataGridViewComboBoxColumn)gvFeeds.Columns["ColCategory"]).Items.Add(item);
            }
            
        }

        private void btCategoryAdd_Click(object sender, EventArgs e) {
            string Kategorinamn = tbKategoryName.Text;
            try {
                CategoriesController.AddCategory(Kategorinamn);
            } catch (ValidationExeption ex) {
                var result = MessageBox.Show(ex.Message);
            }

            tbKategoryName.Clear();


            UpdateCategories();
        }

        private void btFeedAdd_Click(object sender, EventArgs e) {
            string feedUrl = tbURL.Text;
            try {

                FeedsController.AddFeedAsync(new Uri(feedUrl), (Category)cbFeedCategory.SelectedItem);
            } catch (Exception ex) {
                var message = "";
                if (ex is ValidationExeption) {
                    message = ex.Message;
                }
                if (ex is UriFormatException) {
                    message = "Ogiltig URL";
                }
                if (ex is XmlException) {
                    message = "Ogiltig RRS data";
                    
                }
                if (ex is ArgumentException) {
                    message = "Måste ange Kategori";
                    
                }
                var result = MessageBox.Show(message);

            }

            tbURL.Clear();


            UpdateFeedList();
        }
        private void PupulatePodcastList(List<RSSItem> podcasts) {
            lbEpisodes.Items.Clear();
            if (podcasts != null) {
                foreach (var podcast in podcasts)
                {

                    lbEpisodes.Items.Add(podcast);
                }
            }
            
        }

        private void gvFeeds_SelectionChanged(object sender, EventArgs e) {
            if (gvFeeds.SelectedRows.Count != 0) {
                var feed = (RSSFeed)gvFeeds.SelectedRows[0].Tag;


                PupulatePodcastList(feed.Podcasts);
            }
            
        }

        private void lbEpisodes_DoubleClick(object sender, EventArgs e) {
            if (lbEpisodes.SelectedItems.Count == 0) {
                return;
            }
            if (lbEpisodes.SelectedItems[0] != null) {
                var podcast = (RSSItem)lbEpisodes.SelectedItems[0];

                new PodcstItem(podcast);
            }
        }

        private void PodcastPlayerMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var persitanceController = new PersitanceController();
            persitanceController.Write(new PersistantFile(FeedsController.GetFeeds(), CategoriesController.GetCategories()));
           
        }

        private void gvFeeds_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           
            var dgv = gvFeeds;
            if (dgv.Rows.Count < 0) {
                return;
            }
            if (e.RowIndex < 0) {
                return;
            }
            var row = dgv.Rows[e.RowIndex];
            if (row.Tag == null) {
                return;
            }
            
            var rssObject = (RSSFeed)row.Tag;
            rssObject.Title = (String) row.Cells["ColName"].Value;
            var temp = row.Cells["colCategory"].Value;
            
            if (temp is Category)
            {
                rssObject.Category = (Category)temp;
            }
            else if (temp is String) {
                rssObject.Category = CategoriesController.GetCategory((String)temp);
            }
            
            if (int.TryParse((String)row.Cells["ColTimer"].Value, out int value)) {
                rssObject.setUpdateInterval(value);
            } 


            

            UpdateFeedList();
        }

        private void gvFeeds_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void lvCategories_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            var item = (ListViewItem)lvCategories.Items[e.Item];
            var category = (Category)item.Tag;
            
            try {
                Validation.ValidateCategory(new Category(e.Label));
                
                
            }
            catch (Exception ex) {
                e.CancelEdit = true;
                return;
            }
            
            category.Name = e.Label;
            UpdateCategories();

        }

        private void btFeedRemove_Click(object sender, EventArgs e) {
            var dgv = gvFeeds;
            if (dgv.SelectedRows.Count < 1) {
                return;
            }
            if (dgv.SelectedRows[0] == null) {
                return;
            }
            var rowIndex = dgv.SelectedRows[0].Index;

            var feed = (RSSFeed)gvFeeds.Rows[rowIndex].Tag;
            FeedsController.RemoveFeed(feed);

            UpdateFeedList();
            lbEpisodes.Items.Clear();
        }

        private void btCategoryRemove_Click(object sender, EventArgs e) {
            if (lvCategories.SelectedItems[0] == null) {
                return;

            }

            var index = lvCategories.SelectedIndices[0];

            var category = (Category)lvCategories.Items[index].Tag;
            try {
                CategoriesController.RemoveCategory(category);
            } catch (CategoryInUseExeption) {
                MessageBox.Show("Kategorin är i andvändning");
            }
            UpdateCategories();
        }
    }
}
