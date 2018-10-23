using RSSApp.BLL;
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
            categories.AddCategory(Kategorinamn);
            tbKategori.Clear();


            UpdateCategoriesList();

        }
    }
}
