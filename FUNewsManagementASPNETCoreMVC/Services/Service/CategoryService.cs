using BusinessObjects;
using Repositories.Interface;
using Repositories.Repository;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService()
        {
            _categoryRepository = new CategoryRepository();
        }

        public void DeleteCategory(Category a) => _categoryRepository.DeleteCategory(a);

        public List<Category> GetCategories() => _categoryRepository.GetCategories();

        public Category GetCategoryById(int id) => _categoryRepository.GetCategoryById(id);

        public void SaveCategory(Category a) => _categoryRepository.CreateCategory(a);

        public void UpdateCategory(Category a) => _categoryRepository.UpdateCategory(a);
    }
}
