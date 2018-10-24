using RSSApp.DL;
using RSSApp.Exeptions;
using RSSApp.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSApp.BLL {
    class FeedsController {
        private List<RSSFeed> PodcastFeeds = new List<RSSFeed>();


        public FeedsController() {



           
        }
        public void AddFeed(Uri uri, Category category) {
            try {
                var feed = new RSSFetcher(uri).Fetch();
                feed.Category = category;
                AddFeed(feed);
            } catch (ValidationExeption ex) {
                throw ex;
            } 
        }
        public void AddFeed(RSSFeed feed) {
            try {
                Validation.ValidateFeed(feed);
            } catch (ValidationExeption ex) {
                throw ex;
            }
            
            PodcastFeeds.Add(feed);
        }
        

        public List<RSSFeed> GetFeeds() {
            return PodcastFeeds;
        }
    }
}
