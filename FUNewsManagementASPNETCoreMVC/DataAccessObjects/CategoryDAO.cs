using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            var listCategories = new List<Category>();
            try
            {
                using var context = new FUNewsManagementContext();
                listCategories = context.Categories.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listCategories;
        }

        public static void SaveCategory(Category a)
        {
            try
            {
                using var context = new FUNewsManagementContext();
                context.Categories.Add(a);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateCategory(Category a)
        {
            try
            {
                using var context = new FUNewsManagementContext();
                context.Entry<Category>(a).State
                    = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteCategory(Category a)
        {
            try
            {
                using var context = new FUNewsManagementContext();
                context.Remove(a);

                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Category GetCategoryById(int id)
        {
            using var db = new FUNewsManagementContext();
            return db.Categories.FirstOrDefault(c => c.CategoryId == id);
        }
    }
}
