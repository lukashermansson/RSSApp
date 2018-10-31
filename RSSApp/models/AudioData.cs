using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSApp.models {
    public class AudioData {
        public byte[] Data { get; set; }
        public Uri uri { get; set; }


        public AudioData(byte[] Data, Uri uri) {
            this.Data = Data;
            this.uri = uri;
        }
    }
}
