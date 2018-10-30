using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSApp.models {
    public class AudioData {
        public byte[] Data { get; set; }


        public AudioData(byte[] Data) {
            this.Data = Data;

        }
    }
}
