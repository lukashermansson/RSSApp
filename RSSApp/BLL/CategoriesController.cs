using RSSApp.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSApp.BLL {
    public class CategoriesController {

        private List<Category> Categories = new List<Category>();

        public CategoriesController() {
        }

        public void AddCategory(Category category) {
            if (Validation.ValidateCategory(category)) {
                Categories.Add(category);
            } 
        }
        public void AddCategory(string categoryName) {
            var category = new Category(categoryName);
            AddCategory(category);
        }

        public List<Category> GetCategories() {
            return Categories;
        }
    }
}
