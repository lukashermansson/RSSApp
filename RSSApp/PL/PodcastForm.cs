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

namespace RSSApp.PL {
    public partial class PodcastForm : Form {

        private RSSItem podcast;
        public PodcastForm(RSSItem podcast) {
            this.podcast = podcast;
            InitializeComponent();

            lbTitle.Text = podcast.Title;
            lbDescription.Text = podcast.Description;

            this.Show();
        }
    }
}
