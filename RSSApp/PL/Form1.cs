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

namespace RSSApp
{
    public partial class Form1 : Form {
        private FeedsController Feeds{ get; set; }
        private CategoriesController categories { get; set; }

        public Form1()
        {
            Feeds = new FeedsController();
            categories = new CategoriesController();

            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
           
        }

        private void UpdateFeedList() {
            lvFeed.Items.Clear();
            foreach (var Feed in Feeds.GetFeeds()) {
                var item = new ListViewItem(new string[] { Feed.Title, Feed.Podcasts.Count.ToString(), Feed.Category.ToString() });
                item.Tag = Feed;
                lvFeed.Items.Add(item);
            }
        }

        private void UpdateCategoriesList() {
            lvKategorier.Items.Clear();
            foreach (var category in categories.GetCategories()) {
                lvKategorier.Items.Add(category.ToString());
            }
        }

        private void UpdateCategories() {
            UpdateCategoriesComboBox();
            UpdateCategoriesList();
        }

        private void UpdateCategoriesComboBox() {
            cboKategori.Items.Clear();
            foreach (var item in categories.GetCategories()) {
                cboKategori.Items.Add(item);
            }
        }

        private void lvFeed_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnSparaKategori_Click(object sender, EventArgs e) {
            string Kategorinamn = tbKategori.Text;
            try {
                categories.AddCategory(Kategorinamn);
            } catch(ValidationExeption ex){
                var result = MessageBox.Show(ex.Message);
            }
            
            tbKategori.Clear();


            UpdateCategories();

        }

        private void btnSparaPodcast_Click(object sender, EventArgs e) {
            string feedUrl = tbURL.Text;
            try {
                
                Feeds.AddFeed(new Uri(feedUrl), (Category)cboKategori.SelectedItem);
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
                var result = MessageBox.Show(message);
                
            }

            tbURL.Clear();


            UpdateFeedList();
        }

        private void lvFeed_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            var feed = (RSSFeed)e.Item.Tag;


            PupulatePodcastList(feed.Podcasts);
        }

        private void PupulatePodcastList(List<RSSItem> podcasts) {
            lvAvsnitt.Clear();

            foreach (var podcast in podcasts) {
                ListViewItem Item = new ListViewItem(podcast.Title);
                Item.Tag = podcast;
                lvAvsnitt.Items.Add(Item);
            }
        }
    }
}
