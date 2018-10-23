using RSSApp.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSApp.BLL {
    public static class Validation {

        public static bool ValidateCategory(Category category) {
            if (category.Name.Trim(' ') == string.Empty) {
                return false;
            }
            return true;
        }
    }
}
