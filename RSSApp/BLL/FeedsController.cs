﻿using RSSApp.DL;
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
            PodcastFeeds.Add(new RSSFetcher(uri).Fetch());
        }
        public void AddFeed(RSSFeed feed) {
            PodcastFeeds.Add(feed);
        }
        

        public List<RSSFeed> GetFeeds() {
            return PodcastFeeds;
        }
    }
}