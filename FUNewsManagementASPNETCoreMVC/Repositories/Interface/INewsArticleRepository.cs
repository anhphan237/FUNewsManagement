using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface INewsArticleRepository
    {
        void SaveArticle(NewsArticle a);
        void DeleteArticle(NewsArticle a);
        void UpdateArticle(NewsArticle a);
        List<NewsArticle> GetArticles();
        NewsArticle GetArticleById(int id);
    }
}
