using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSApp.Exeptions {
    public class ValidationExeption : Exception {
        public ValidationExeption() : base() { }
        public ValidationExeption(string message) : base(message) { }
        

    }
}
