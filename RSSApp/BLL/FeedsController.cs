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
        public void AddFeed(Uri uri) {
            try {
                AddFeed(new RSSFetcher(uri).Fetch());
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
