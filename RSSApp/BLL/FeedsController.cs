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
        private static List<RSSFeed> PodcastFeeds = new List<RSSFeed>();

        
      
        public static void AddFeed(Uri uri, Category category) {
            if (category == null) {
                throw new ArgumentException();
            }
            try {
                var feed = new RSSFetcher(uri).Fetch();
                feed.Category = category;
                AddFeed(feed);
            } catch (ValidationExeption ex) {
                throw ex;
            } 
        }
        public static void AddFeed(RSSFeed feed) {
            try {
                Validation.ValidateFeed(feed);
            } catch (ValidationExeption ex) {
                throw ex;
            }
            
            PodcastFeeds.Add(feed);
        }
        

        public static List<RSSFeed> GetFeeds() {
            return PodcastFeeds;
        }
    }
}
