using RSSApp.DL;
using RSSApp.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSSApp.BLL
{
    class PersitanceController
    {

        public string URL;
        private string fileName = "RSSAPPsave.xml";

        public PersitanceController() {

            var currentDir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            URL = Path.Combine(currentDir, fileName);
            

            
        }
        public PersistantFile Read() {
            var xmlString = "";
            var reader = new XMLReader(URL);
            try {
                xmlString = reader.read();
            } catch (FileNotFoundException ex) {
                
            }



            var items = new PersistantFile().DeSerialize(xmlString);

            for(int i = 0; i < items.feeds.Count; i++) {
                var feed = items.feeds[i];
                var fetcher = new RSSFetcher(feed.URI);

                var onlineFeed = fetcher.Fetch();

                feed.Podcasts = onlineFeed.Podcasts;
                
            }

            return items;
        }


        public void Write(PersistantFile persistantFile) {

            var xml = persistantFile.Serialize();
            var writer = new XMLWriter(xml, URL);

            writer.WriteXML();
        }
    }
}
