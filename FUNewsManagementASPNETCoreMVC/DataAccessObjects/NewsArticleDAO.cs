using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class NewsArticleDAO
    {
        public static List<NewsArticle> GetNewsArticles()
        {
            var articleProducts = new List<NewsArticle>();
            try
            {
                using var db = new FUNewsManagementContext();
                articleProducts = db.NewsArticles.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return articleProducts;
        }

        public static void SaveNewsArticle(NewsArticle a)
        {
            try
            {
                using var context = new FUNewsManagementContext();
                context.NewsArticles.Add(a);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateNewsArticle(NewsArticle a)
        {
            try
            {
                using var context = new FUNewsManagementContext();
                context.Entry<NewsArticle>(a).State
                    = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteNewsArticle(NewsArticle a)
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

        public static NewsArticle GetNewsArticleById(int id)
        {
            using var db = new FUNewsManagementContext();
            return db.NewsArticles.FirstOrDefault(c => c.NewsArticleId == id.ToString());
        }

        public static NewsArticle GetNewsArticleDetailById(int id)
        {
            using var db = new FUNewsManagementContext();
            return db.NewsArticles
                .Include(na => na.CreatedBy)   // Nạp CreatedBy
                .Include(na => na.UpdatedBy)   // Nạp UpdatedBy
                .Include(na => na.Category)    // Nạp Category
                .FirstOrDefault(c => c.NewsArticleId == id.ToString());
        }

        public static List<NewsArticle> GetNewsArticlesDetail()
        {
            var articleProducts = new List<NewsArticle>();
            try
            {
                using var db = new FUNewsManagementContext();

                // Sử dụng Include để nạp các mối quan hệ CreatedBy và UpdatedBy
                articleProducts = db.NewsArticles
                    .Include(na => na.CreatedBy)   // Nạp CreatedBy
                    .Include(na => na.UpdatedBy)   // Nạp UpdatedBy
                    .Include(na => na.Category)    // Nạp Category
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return articleProducts;
        }

        public static int GetNumberOfNewsArticle()
        {
            using var db = new FUNewsManagementContext();
            return db.NewsArticles.Count();
        }
    }

}
