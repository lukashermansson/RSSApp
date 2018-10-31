using RSSApp.DL;
using RSSApp.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSSApp.BLL {

    public class DownloadAndWriteCompletedArgs : EventArgs {
        public AudioData audioData;
    }
    public class DownloadController {


        string currentDir;



        public delegate void DownloadAndWriteCompletedHandler(object sender, DownloadAndWriteCompletedArgs e);

        public event DownloadAndWriteCompletedHandler DownloadAndWriteCompleted;
        public DownloadController() {

           currentDir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            

        }

        public async void DownloadAsync(Uri fetchURL) {

            var fetcher = new AudioFetcher(fetchURL);
            fetcher.DownloadCompleted += DownloadCompletedEvent;
            fetcher.FetchAsync();

            
        }
        

        void DownloadCompletedEvent(object sender, DownloadDataCompletedEventArgs e) {
            var pathtoFile = currentDir + "/Audio." + ((Uri)e.UserState).Segments.Last().Split('.')[1];
            var audioData = new AudioData(e.Result, new Uri(pathtoFile));
            
            AudioWriter.ByteArrayToFile(audioData.uri.AbsolutePath, audioData.Data);

            
            OnDownloadAndWriteCompleted(audioData);
        }

        protected virtual void OnDownloadAndWriteCompleted(AudioData data) {
           
            if (DownloadAndWriteCompleted != null)
                DownloadAndWriteCompleted(this, new DownloadAndWriteCompletedArgs() {  audioData = data });
        }


    }
}
