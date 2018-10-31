using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSApp.Exeptions {
    class CategoryInUseExeption : Exception{

        public CategoryInUseExeption() : base() { }
        public CategoryInUseExeption(string message) : base(message) { }
    }
}
