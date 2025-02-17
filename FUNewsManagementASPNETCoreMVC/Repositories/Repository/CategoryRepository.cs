using BusinessObjects;
using DataAccessObjects;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public void CreateCategory(Category a) => CategoryDAO.SaveCategory(a);

        public void DeleteCategory(Category a) => CategoryDAO.DeleteCategory(a);

        public List<Category> GetCategories() => CategoryDAO.GetCategories();

        public Category GetCategoryById(int id) => CategoryDAO.GetCategoryById(id);

        public void UpdateCategory(Category a) => CategoryDAO.UpdateCategory(a);
    }
}
