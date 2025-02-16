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
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository _newsArticleRepository;

        public NewsArticleService()
        {
            _newsArticleRepository = new NewsArticleRepository();
        }

        public void DeleteNewsArticle(NewsArticle a) => _newsArticleRepository.DeleteArticle(a);

        public NewsArticle GetNewsArticleById(int id) => _newsArticleRepository.GetArticleById(id);

        public List<NewsArticle> GetNewsArticles() => _newsArticleRepository.GetArticles();

        public void SaveNewsArticle(NewsArticle a) => SaveNewsArticle(a);

        public void UpdateNewsArticle(NewsArticle p) => _newsArticleRepository.UpdateArticle(p);    
    }
}
