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
            return db.NewsArticles.FirstOrDefault(c => c.NewsArticleId.Equals(id));
        }
    }

}
