using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface INewsArticleService
    {
        void SaveNewsArticle(NewsArticle a);
        void DeleteNewsArticle(NewsArticle a);
        void UpdateNewsArticle(NewsArticle a);
        List<NewsArticle> GetNewsArticles();
        List<NewsArticle> GetNewsArticlesDetail();
        NewsArticle GetNewsArticleDetailById(int id);
        int GetNumberOfArticle();
    }
}
