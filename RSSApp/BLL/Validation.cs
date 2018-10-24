using RSSApp.Exeptions;
using RSSApp.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSApp.BLL {
    public static class Validation {

        public static void ValidateCategory(Category category) {
            if (category.Name.Trim(' ') == string.Empty) {
                throw new ValidationExeption("Kategori kan inte vara tom");
            }
            return;
        }

        public static void ValidateFeed(RSSFeed feed) {
            if (feed.URI.ToString().Trim(' ') == string.Empty) {
                throw new ValidationExeption("Feed url kan inte vara tom");
            }
            return;
        }
    }
}
