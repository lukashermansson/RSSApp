

using System;

namespace RSSApp {

    public class MP3Fetcher : IFetch<AudioData> {
        public Uri FetchURL { get; set; }

        public AudioData Fetch() {
            throw new System.NotImplementedException();
        }
    }
}
