namespace RSSApp {
    using System;

    public class RSSFetcher : IFetch<RSSFeed> {
        public String FetchURL { get; set; }

        public RSSFetcher(String FetchURL) {
            this.FetchURL = FetchURL;
        }

        public RSSFeed Fetch() {

        }
    }
}

