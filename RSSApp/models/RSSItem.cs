
using System;

namespace RSSApp {


    public class RSSItem {

        public String Title { get; set; }
        public String Description { get; set; }
        public String MediaURL { get; set; }
        public DateTime PublisedDate { get; set; }


        public RSSItem() {

        }


        public RSSItem(String Title) {
            this.Title = Title;
        }
        public RSSItem(String Title, String Description) {
            this.Title = Title;
        }
    }
}

