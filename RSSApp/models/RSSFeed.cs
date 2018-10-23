using RSSApp.models;
using System;
using System.Collections.Generic;

namespace RSSApp.models {

    public class RSSFeed {

        public Uri URI { get; set; }
        public List<RSSItem> Podcasts { get; set; }
        public String Title { get; set; }
        public Category Category { get; set; }



        public RSSFeed(Uri URI) {
            this.URI = URI;
            Podcasts = new List<RSSItem>();
        }


    }
}
