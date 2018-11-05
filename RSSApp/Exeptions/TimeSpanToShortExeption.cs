using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSApp.Exeptions {
    class TimeSpanToShortExeption : Exception{
        public TimeSpanToShortExeption() : base() { }
        public TimeSpanToShortExeption(string message) : base(message) { }
    }
}
