using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Services.Interface;
using FUNewsManagement.Filters;

namespace FUNewsManagement.Controllers
{
    [SessionCheck] // Chặn truy cập nếu chưa đăng nhập
    public class NewsArticlesController : Controller
    {
        private readonly INewsArticleService _contextNewsArticle;
        private readonly ICategoryService _contextCategory;

        public NewsArticlesController(INewsArticleService contextNewsArticle, ICategoryService contextCategory)
        {
            _contextNewsArticle = contextNewsArticle;
            _contextCategory = contextCategory;
        }


        // GET: NewsArticles
        public async Task<IActionResult> Index()
        {
            ViewBag.Role = HttpContext.Session.GetInt32("AccountRole");
            var myStoreContext = _contextNewsArticle.GetNewsArticles();
            return View(myStoreContext.ToList());
        }

        // GET: NewsArticles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Role = HttpContext.Session.GetInt32("AccountRole");

            var product = _contextNewsArticle.GetNewsArticleById(Convert.ToInt32(id));
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: NewsArticles/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_contextCategory.GetCategories(), "CategoryId", "CategoryId");
            return View();
        }

        // POST: NewsArticles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsArticleId,NewsTitle,Headline,CreatedDate,NewsContent,NewsSource,CategoryId,NewsStatus,CreatedById,UpdatedById,ModifiedDate")] NewsArticle newsArticle)
        {
            if (ModelState.IsValid)
            {
                _contextNewsArticle.SaveNewsArticle(newsArticle);

                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_contextCategory.GetCategories(), "CategoryId", "CategoryId", newsArticle.CategoryId);
            return View(newsArticle);
        }

        // GET: NewsArticles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = _contextNewsArticle.GetNewsArticleById(Convert.ToInt32(id));
            if (article == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_contextCategory.GetCategories(), "CategoryId", "CategoryId", article.CategoryId);
            return View(article);
        }

        // POST: NewsArticles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewsArticleId,NewsTitle,Headline,CreatedDate,NewsContent,NewsSource,CategoryId,NewsStatus,CreatedById,UpdatedById,ModifiedDate")] NewsArticle newsArticle)
        {
            if (id.ToString() != newsArticle.NewsArticleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contextNewsArticle.UpdateNewsArticle(newsArticle);
                }
                catch (Exception)
                {
                    if (!NewsArticleExists(Convert.ToInt32(newsArticle.NewsArticleId)))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_contextCategory.GetCategories(), "CategoryId", "CategoryId", newsArticle.CategoryId);
            return View(newsArticle);
        }

        // GET: NewsArticles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = _contextNewsArticle.GetNewsArticleById(Convert.ToInt32(id));
            if (newsArticle == null)
            {
                return NotFound();
            }

            return View(newsArticle);
        }

        // POST: NewsArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsArticle = _contextNewsArticle.GetNewsArticleById(Convert.ToInt32(id));
            if (newsArticle != null)
            {
                _contextNewsArticle.DeleteNewsArticle(newsArticle);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool NewsArticleExists(int id)
        {
            var tmp = _contextNewsArticle.GetNewsArticleById(Convert.ToInt32(id));
            return (tmp != null) ? true : false;
        }
    }
}
