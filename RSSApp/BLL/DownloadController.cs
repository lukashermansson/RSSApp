using RSSApp.DL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSSApp.BLL {
    class DownloadController {


        public Uri URI;
        private string fileName = "Audio.mp3";

        public DownloadController() {

            var currentDir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            URI = new Uri(Path.Combine(currentDir, fileName));

        }

        public void Download(Uri fetchURL) {

            var fetcher = new MP3Fetcher(fetchURL);
            var bytes = fetcher.Fetch();

            AudioWriter.ByteArrayToFile(URI.AbsolutePath, bytes.Data);
        }
    }
}
