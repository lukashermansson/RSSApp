using System;

public class RSSItem {

    public String Title { get; set; }
    public String Description { get; set; }
    public String MediaURL { get; set; }
    public DateTime PublisedDate { get; set; }


    public RSSFeed() {

    }


    public RSSFeed(String Title) {
        this.Title = Title;
    }
    public RSSFeed(String Title, String Description) {
        this.Title = Title;
    }
}
