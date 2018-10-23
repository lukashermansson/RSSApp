using System;

public class RSSFeed{

    public String URL { get; set; }
    public List<RSSItem> Podcasts { get; set; }


    public RSSFeed(String URL) {
        this.URL = URL;
        Podcasts = new List<RSSItem>();
	}

    
}
