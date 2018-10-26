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

namespace RSSApp.PL
{
    public partial class PodcstItem : Form
    {

        private RSSItem podcastEpisode;
        public PodcstItem(RSSItem podcastItem)
        {
            podcastEpisode = podcastItem;
            InitializeComponent();
            lbTitle.Text = podcastEpisode.Title;
            rtbDescription.Text = podcastEpisode.Description;


            this.Show();
        }

        private void PodcstItem_Load(object sender, EventArgs e)
        {

        }
    }
}
