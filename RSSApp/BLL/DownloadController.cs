using RSSApp.DL;
using RSSApp.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSSApp.BLL {

    public class DownloadAndWriteCompletedArgs : EventArgs {
        public AudioData audioData;
    }
    public class DownloadController {


        public Uri URI;
        private string fileName = "Audio.mp3";

        public delegate void DownloadAndWriteCompletedHandler(object sender, DownloadAndWriteCompletedArgs e);

        public event DownloadAndWriteCompletedHandler DownloadAndWriteCompleted;
        public DownloadController() {

            var currentDir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            URI = new Uri(Path.Combine(currentDir, fileName));

        }

        public async void DownloadAsync(Uri fetchURL) {

            var fetcher = new MP3Fetcher(fetchURL);
            fetcher.DownloadCompleted += DownloadCompletedEvent;
            fetcher.FetchAsync();

            
        }


        void DownloadCompletedEvent(object sender, DownloadDataCompletedEventArgs e) {
            var audioData = new AudioData(e.Result, URI);
            AudioWriter.ByteArrayToFile(audioData.uri.AbsolutePath, audioData.Data);

            
            OnDownloadAndWriteCompleted(audioData);
        }

        protected virtual void OnDownloadAndWriteCompleted(AudioData data) {
           
            if (DownloadAndWriteCompleted != null)
                DownloadAndWriteCompleted(this, new DownloadAndWriteCompletedArgs() {  audioData = data });
        }


    }
}
