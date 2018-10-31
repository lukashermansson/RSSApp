

using RSSApp.models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RSSApp.DL {
    
    public class MP3Fetcher : IFetch<AudioData> {
        public Uri FetchURL { get; set; }

        
        public delegate void DownloadDataCompletedEventHandler(object sender, DownloadDataCompletedEventArgs e);

        // Declare the event.
        public event DownloadDataCompletedEventHandler DownloadCompleted;

        public MP3Fetcher(Uri uri) {
            FetchURL = uri;
        }

        public AudioData Fetch() {
            byte[] audioBytes;
            using (WebClient webClient = new WebClient()) {
                audioBytes  = webClient.DownloadData(FetchURL);
            }

            return new AudioData(audioBytes, FetchURL);
        }
        public async Task FetchAsync() {
            
            using (WebClient webClient = new WebClient()) {
                webClient.DownloadDataCompleted += DownloadDataCompletedFrom;
                webClient.DownloadDataAsync(FetchURL);
            }

            
        }
        protected virtual void OnDownloadCompleted(object sender, DownloadDataCompletedEventArgs e) {

            if (DownloadCompleted != null) {
                DownloadCompleted(this, e);
            }
        }
        public void DownloadDataCompletedFrom(object sender, DownloadDataCompletedEventArgs e) {
            OnDownloadCompleted(sender, e);


        }

    }
}
