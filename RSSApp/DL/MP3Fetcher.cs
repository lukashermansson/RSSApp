

using RSSApp.models;
using System;

namespace RSSApp.DL {

    public class MP3Fetcher : IFetch<AudioData> {
        public Uri FetchURL { get; set; }

        public AudioData Fetch() {
            throw new System.NotImplementedException();
        }
    }
}
