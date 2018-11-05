using RSSApp.BLL;
using RSSApp.models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace RSSApp.models {

    public class RSSFeed : IXmlSerializable {
        private int UpdateInterval = 300000;
        public Uri URI { get; set; }
        public List<RSSItem> Podcasts { get; set; }
        public String Title { get => _title; set => _title = value; }
        public Category Category { get; set; }
        private string CategoryName;
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private string _title;

        public delegate void OnTimerTickEventHandler(object sender, EventArgs e);

        // Declare the event.
        public event OnTimerTickEventHandler TimerTick;

        public RSSFeed() { }
        public RSSFeed(Uri URI) {
            this.URI = URI;
            Podcasts = new List<RSSItem>();
            
            
        }

        public void StartTimer() {
            timer.Interval = UpdateInterval;
            timer.Tick += onTimerTick;
            timer.Enabled = true;
        }
        public int getUpdateInterval(){
            return UpdateInterval;
        }
        public void setUpdateInterval(int value) {
            UpdateInterval = value;

            UpdateTimer();
        }

        private void UpdateTimer() {
            timer.Interval = UpdateInterval;
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
            try {
                var interval = reader.GetAttribute("UpdateInterval");
                UpdateInterval = int.Parse(interval);
            } catch {
                
            }


            reader.Skip();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Title", Title);
            writer.WriteAttributeString("Category", Category.Name);
            writer.WriteAttributeString("URI", URI.AbsoluteUri);
            writer.WriteAttributeString("UpdateInterval", UpdateInterval.ToString());
        }

        protected virtual void onTimerTick(object sender, EventArgs e) {
            if (TimerTick != null)
                TimerTick(this, EventArgs.Empty);
            
        }
    }
}
