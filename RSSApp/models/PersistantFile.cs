using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace RSSApp.models
{
    public class PersistantFile
    {
        public List<Category> Categories;
        public List<RSSFeed> feeds;


        public PersistantFile(List<RSSFeed> feeds, List<Category> categories)
        {
            this.feeds = feeds;
            this.Categories = categories;
        }
        public PersistantFile()
        {
            this.Categories = new List<Category>();
            this.feeds = new List<RSSFeed>();
        }

        public string Serialize()
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(PersistantFile));


            using (var stringWriter = new StringWriter())
            {
                using (XmlWriter XMLWriter = XmlWriter.Create(stringWriter))
                {
                    xsSubmit.Serialize(stringWriter, this);
                    return stringWriter.ToString();
                }
            }
        }
        public PersistantFile DeSerialize(string serializedString)
        {

            XmlSerializer xsSubmit = new XmlSerializer(typeof(PersistantFile));


            using (var stringReader = new StringReader(serializedString))
            {

                try
                {
                    return xsSubmit.Deserialize(stringReader) as PersistantFile;
                }
                catch (InvalidOperationException ex) {
                    Console.WriteLine(ex);
                    return new PersistantFile();
                }
                
                
            }
        }
    }
}
