namespace RSSApp.DL {
    using RSSApp.models;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel.Syndication;
    using System.Threading.Tasks;
    using System.Xml;

    public class RSSFetcher : IFetch<RSSFeed> {
        public Uri FetchURL { get; set; }

        public RSSFetcher(Uri FetchURL) {
            this.FetchURL = FetchURL;
        }

        public RSSFeed Fetch() {
            
            XmlReader reader = XmlReader.Create(FetchURL.ToString());
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            RSSFeedFromSyndicationItem(feed);

            
            return RSSFeedFromSyndicationItem(feed);
        }
       

        private RSSFeed RSSFeedFromSyndicationItem(SyndicationFeed feed) {
            RSSFeed Output = new RSSFeed(FetchURL);
            Output.Title = feed.Title.Text;

            Output.Podcasts = RSSItemsFromSyndicationItems(feed.Items);

            return Output;
        }

        private List<RSSItem> RSSItemsFromSyndicationItems(IEnumerable<SyndicationItem> list) {
            List<RSSItem> output = new List<RSSItem>();

            foreach (var item in list) {
                output.Add(RSSItemFromSyndicationItem(item));
            }

            return output;
        }


        private RSSItem RSSItemFromSyndicationItem(SyndicationItem item) {
            RSSItem output = new RSSItem();
            output.Title = item.Title.Text;
            output.Description = item.Summary.Text;
            output.PublisedDate = item.PublishDate.DateTime;
            //todo: playURL
            output.PlayURL = item.Links[1].Uri;

            return output;
        }

    }
}

