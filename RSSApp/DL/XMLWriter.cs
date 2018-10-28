using RSSApp.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace RSSApp.DL
{
    class XMLWriter
    {

       public string XML;
        public string path;

        public XMLWriter(string XML, string path) {
            this.XML = XML;
            this.path = path;
        }

       public void WriteXML()
        {
            var fs = new FileStream(
                path,
                FileMode.Create,
                FileAccess.Write
            );

            var streamWriter = new StreamWriter(fs);


            streamWriter.Write(XML);

            streamWriter.Close();
            fs.Close();
            
        }
        
    }
}
