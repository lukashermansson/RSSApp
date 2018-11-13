using RSSApp.BLL;
using RSSApp.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            wbDescription.DocumentText = podcastEpisode.Description;


            

            this.Show();
        }

        private void PodcstItem_Load(object sender, EventArgs e)
        {

        }

        private void btPlay_Click(object sender, EventArgs e) {
            btPlay.Enabled = false;
            var controller = new DownloadController();
            controller.DownloadAndWriteCompleted += DownloadCompleted;
            controller.Download(podcastEpisode.PlayURL);
        }

        void DownloadCompleted(object sender, DownloadAndWriteCompletedArgs e) {
            Process proc = new Process();
            proc.StartInfo.FileName = e.audioData.uri.AbsolutePath;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
            btPlay.Enabled = true;
        }

        private void wbDescription_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
            wbDescription.Document.BackColor = Color.FromKnownColor(KnownColor.Control);
        }
    }
}
