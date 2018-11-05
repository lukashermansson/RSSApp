using RSSApp.Exeptions;
using RSSApp.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSApp.BLL {
    public class CategoriesController {

        private static List<Category> Categories = new List<Category>();

 

        public static void AddCategory(Category category) {
            try {
                Validation.ValidateCategory(category);
            } catch (ValidationExeption ex) {
                throw ex;
                
            }
            
            Categories.Add(category);


        }
        public static void AddCategory(string categoryName) {
            var category = new Category(categoryName);
            
            try {
                AddCategory(category);
            } catch (ValidationExeption ex) {
                throw ex;
            }
        }
        public static Category GetCategory(string Name) {
            foreach (var category in Categories) {
                if (category.Name == Name) {
                    return category;
                }
            }
            var newCategory = new Category(Name);
            AddCategory(newCategory);
            return newCategory;
        }
        public static List<Category> GetCategories() {

            var Query =
                    from categoty in Categories
                    orderby categoty.Name ascending
                select categoty;

            var sortedList = new List<Category>();
            foreach (var cat in Query) {
                sortedList.Add(cat);
            }
            return sortedList;
        }

        public static void RemoveCategory(Category category) {
            var feeds = FeedsController.GetFeeds();
            foreach (var feed in feeds) {
                if (feed.Category == category) {
                    
                    throw new CategoryInUseExeption();
                    
                }
            }
            Categories.Remove(category);
        }


      
    }
}
