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
    public class NewsArticleRepository : INewsArticleRepository
    {
        public void DeleteArticle(NewsArticle a) => NewsArticleDAO.DeleteNewsArticle(a);

        public NewsArticle GetArticleDetailById(int id) => NewsArticleDAO.GetNewsArticleDetailById(id);

        public List<NewsArticle> GetArticles() => NewsArticleDAO.GetNewsArticles();

        public List<NewsArticle> GetNewsArticlesDetail() => NewsArticleDAO.GetNewsArticlesDetail();

        public int GetNumberOfArticle() => NewsArticleDAO.GetNumberOfNewsArticle();

        public void SaveArticle(NewsArticle a) => NewsArticleDAO.SaveNewsArticle(a);

        public void UpdateArticle(NewsArticle a) => NewsArticleDAO.UpdateNewsArticle(a);
    }
}
