using RSSApp.BLL;
using RSSApp.models;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace RSSApp.models {

    public class RSSFeed : IXmlSerializable
    {

        public Uri URI { get; set; }
        public List<RSSItem> Podcasts { get; set; }
        public String Title { get => _title; set => _title = value; }
        public Category Category { get; set; }
        private string CategoryName;
        private string _title;

        public RSSFeed() { }
        public RSSFeed(Uri URI)
        {
            this.URI = URI;
            Podcasts = new List<RSSItem>();


        }

        
        public void InitializeCategory()
        {
            Category = CategoriesController.GetCategory(CategoryName);
        }
        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader reader)
        {


            Title = reader.GetAttribute("Title");
            CategoryName = reader.GetAttribute("Category");
            URI = new Uri(reader.GetAttribute("URI"));

            reader.Skip();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Title", Title);
            writer.WriteAttributeString("Category", Category.Name);
            writer.WriteAttributeString("URI", URI.AbsoluteUri);
        }
    }
}
