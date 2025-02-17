using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        void DeleteCategory(Category a);
        void UpdateCategory(Category a);
        void CreateCategory(Category a);
        Category GetCategoryById(int id);
    }
}
