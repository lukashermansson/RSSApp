

namespace RSSApp {

    public class MP3Fetcher : IFetch<AudioData> {
        public string FetchURL { get; set; }

        public AudioData Fetch() {
            throw new System.NotImplementedException();
        }
    }
}
