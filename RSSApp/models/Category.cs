using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSApp.models {
    public class Category {
        public String Name { get; set; }

        public Category(string categoryName) {
            this.Name = categoryName;
        }
        public Category() {
        }
        public override string ToString() {
            return Name;
        }



    }
}
