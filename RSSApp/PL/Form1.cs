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

namespace RSSApp
{
    public partial class Form1 : Form {
        private FeedsController Feeds{ get; set; }
        private CategoriesController categories { get; set; }

        public Form1()
        {
            Feeds = new FeedsController();
            categories = new CategoriesController();

            categories.AddCategory(new Category("Action"));
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //cboKategori.Items.Add(categories.GetCategories());
           
        }

        private void UpdateFeedList() {
            lvFeed.Items.Clear();
            foreach (var Feed in Feeds.GetFeeds()) {
                lvFeed.Items.Add(new ListViewItem(new string[] { Feed.Title, Feed.Podcasts.Count.ToString(), Feed.Category.ToString() }));
            }
        }

        private void UpdateCategoriesList() {
            lvKategorier.Items.Clear();
            foreach (var category in categories.GetCategories()) {
                lvKategorier.Items.Add(category.ToString());
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


            UpdateCategoriesList();

        }

        private void btnSparaPodcast_Click(object sender, EventArgs e) {
            string feedUrl = tbURL.Text;
            try {
                Feeds.AddFeed(new Uri(feedUrl));
            } catch (Exception ex) {
                var message = "";
                if (ex is ValidationExeption) {
                    message = ex.Message;
                }
                if (ex is UriFormatException) {
                    message = "Url kan inte vara tom";
                }
                var result = MessageBox.Show(message);
                
            }

            tbURL.Clear();


            UpdateFeedList();
        }

        private void cboKategori_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
