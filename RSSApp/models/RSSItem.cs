
using System;

namespace RSSApp.models {


    public class RSSItem {

        public String Title { get; set; }
        public String Description { get; set; }
        public DateTime PublisedDate { get; set; }
        public Uri PlayURL { get; set; }




        public RSSItem() {

        }
        public override String ToString() {
            return Title;
        }

        public RSSItem(String Title) {
            this.Title = Title;
        }
        public RSSItem(String Title, String Description) {
            this.Title = Title;
        }
    }
}

