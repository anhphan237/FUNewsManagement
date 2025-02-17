using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        void SaveCategory(Category a);
        void DeleteCategory(Category a);
        void UpdateCategory(Category a);
        Category GetCategoryById(int id);
    }
}
