using RSSApp.Exeptions;
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
            try {
                Validation.ValidateCategory(category);
            } catch (ValidationExeption ex) {
                throw ex;
                
            }
            Categories.Add(category);


        }
        public void AddCategory(string categoryName) {
            var category = new Category(categoryName);
            
            try {
                AddCategory(category);
            } catch (ValidationExeption ex) {
                throw ex;
            }
        }

        public List<Category> GetCategories() {
            return Categories;
        }
    }
}
