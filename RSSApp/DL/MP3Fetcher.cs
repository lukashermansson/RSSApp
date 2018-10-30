

using RSSApp.models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace RSSApp.DL {

    public class MP3Fetcher : IFetch<AudioData> {
        public Uri FetchURL { get; set; }

        public MP3Fetcher(Uri uri) {
            FetchURL = uri;
        }

        public AudioData Fetch() {
            byte[] audioBytes;
            using (WebClient webClient = new WebClient()) {
                audioBytes  = webClient.DownloadData(FetchURL);
            }

            return new AudioData(audioBytes);
        }
        public async Task<AudioData> FetchAsync() {
            byte[] audioBytes;
            using (WebClient webClient = new WebClient()) {
                audioBytes = webClient.DownloadData(FetchURL);
            }

            return new AudioData(audioBytes);
        }
    }
}
