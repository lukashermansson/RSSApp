using RSSApp.DLL;
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
        private FeedsController feeds{ get; set; }

        public Form1()
        {
            feeds = new FeedsController();
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (RSSFeed Feed in feeds.GetFeeds()) {
                lvFeed.Items.Add(new ListViewItem(new string[] { Feed.Title, "13", "Fight"}));
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
    }
}
