using RSSApp.BLL;
using RSSApp.DL;
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
    public partial class EditFeed : Form {
        public RSSFeed feed;

        public delegate void UpdateEventHandler(object sender, EventArgs e);

        // Declare the event.
        public event UpdateEventHandler Updated;


        public EditFeed(RSSFeed feed) {

            this.feed = feed;
            InitializeComponent();

            this.Show();
        }

        private void EditFeed_Load(object sender, EventArgs e) {
            lbTitle.Text = feed.Title;
            tbURL.Text = feed.URI.AbsoluteUri;

        }
        private void UpdateUI() {
            lbTitle.Text = feed.Title;
            tbURL.Text = feed.URI.AbsoluteUri;

            OnUpdated();

        }
        private void btSpara_Click(object sender, EventArgs e) {
            var newURI = new Uri(tbURL.Text);

            try {
                var newfeed = new RSSFetcher(newURI).Fetch();



                feed.Title = newfeed.Title;
                feed.Podcasts = newfeed.Podcasts;
                feed.URI = newfeed.URI;
            } catch (Exception ex) {
                var message = "";

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

            UpdateUI();
        }

        protected virtual void OnUpdated() {

            if (Updated != null) {
                Updated(this, EventArgs.Empty);
            }
        }
    }
}
