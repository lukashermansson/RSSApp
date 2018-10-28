using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSApp.DL
{
    class XMLReader
    {

        public string path;
        public XMLReader(string path) {
            this.path = path;
        }
        public string read()
        {
            FileStream fs;
            try
            {
                fs = new FileStream(
                    path,
                    FileMode.Open,
                    FileAccess.Read
                );
            }catch(FileNotFoundException ex) {
                throw ex;
            }
           
           
            var sr = new StreamReader(fs);

            var xmlString = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            return xmlString;
        }
    }
}
