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


        private FeedsController Feeds { get; set; }
        private CategoriesController categories { get; set; }
        public PodcastPlayerMainForm() {
            Feeds = new FeedsController();
            categories = new CategoriesController();

            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) {

        }

        private void UpdateFeedList() {
            gvFeeds.Rows.Clear();
            foreach (var Feed in Feeds.GetFeeds()) {
                int row = gvFeeds.Rows.Add();
                gvFeeds.Rows[row].Cells["ColName"].Value = Feed.Title;
                gvFeeds.Rows[row].Cells["ColNumEpisodes"].Value = Feed.Podcasts.Count;
                gvFeeds.Rows[row].Cells["ColCategory"].Value = Feed.Category.Name;
                gvFeeds.Rows[row].Tag = Feed;
            }
        }

        private void UpdateCategoriesList() {
            lbCategories.Items.Clear();
            foreach (var category in categories.GetCategories()) {
                lbCategories.Items.Add(category.ToString());
            }
        }

        private void UpdateCategories() {
            UpdateCategoriesComboBox();
            UpdateCategoriesList();
        }

        private void UpdateCategoriesComboBox() {
            cbFeedCategory.Items.Clear();
            foreach (var item in categories.GetCategories()) {
                cbFeedCategory.Items.Add(item);
            }
        }

        private void btCategoryAdd_Click(object sender, EventArgs e) {
            string Kategorinamn = tbKategoryName.Text;
            try {
                categories.AddCategory(Kategorinamn);
            } catch (ValidationExeption ex) {
                var result = MessageBox.Show(ex.Message);
            }

            tbKategoryName.Clear();


            UpdateCategories();
        }

        private void btFeedAdd_Click(object sender, EventArgs e) {
            string feedUrl = tbURL.Text;
            try {

                Feeds.AddFeed(new Uri(feedUrl), (Category)cbFeedCategory.SelectedItem);
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

            foreach (var podcast in podcasts) {
                
                lbEpisodes.Items.Add(podcast);
            }
        }

        private void gvFeeds_SelectionChanged(object sender, EventArgs e) {
            if (gvFeeds.SelectedRows.Count != 0) {
                var feed = (RSSFeed)gvFeeds.SelectedRows[0].Tag;


                PupulatePodcastList(feed.Podcasts);
            }
            
        }

        private void lbEpisodes_DoubleClick(object sender, EventArgs e) {
            if (lbEpisodes.SelectedItems[0] != null) {
                var podcast = (RSSItem)lbEpisodes.SelectedItems[0];

                new PodcastForm(podcast);
            }
        }
    }
}
