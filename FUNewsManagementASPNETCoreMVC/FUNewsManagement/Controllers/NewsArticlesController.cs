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
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Services.Service;
using System.Data;

namespace FUNewsManagement.Controllers
{
    [SessionCheck] // Chặn truy cập nếu chưa đăng nhập
    public class NewsArticlesController : Controller
    {
        private readonly INewsArticleService _contextNewsArticle;
        private readonly ICategoryService _contextCategory;
        private readonly ISystemAccountService _contextAccount;

        public NewsArticlesController(
            INewsArticleService contextNewsArticle, 
            ICategoryService contextCategory,
            ISystemAccountService contextAccount)
        {
            _contextNewsArticle = contextNewsArticle;
            _contextCategory = contextCategory;
            _contextAccount = contextAccount;
        }


        // GET: NewsArticles
        public async Task<IActionResult> Index()
        {
            ViewBag.Role = HttpContext.Session.GetInt32("AccountRole");
            var newsArticles = _contextNewsArticle.GetNewsArticlesDetail();
            return View(newsArticles.ToList());
        }

        // GET: NewsArticles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Role = HttpContext.Session.GetInt32("AccountRole");

            var newsArticles = _contextNewsArticle.GetNewsArticleDetailById(Convert.ToInt32(id));
            if (newsArticles == null)
            {
                return NotFound();
            }

            return View(newsArticles);
        }

        // GET: NewsArticles/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_contextCategory.GetCategories(), "CategoryId", "CategoryName");
            // Lấy danh sách tài khoản để chọn CreatedBy và UpdatedBy
            ViewBag.CreatedById = new SelectList(_contextAccount.GetAdminAccounts(), "AccountId", "AccountName");
            ViewBag.UpdatedById = new SelectList(_contextAccount.GetAdminAccounts(), "AccountId", "AccountName");
            return View();
        }

        // POST: NewsArticles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsArticleId,NewsTitle,Headline,CreatedDate,NewsContent,NewsSource,CategoryId,NewsStatus,CreatedById,UpdatedById,ModifiedDate")] NewsArticle newsArticle)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // If necessary, manually set NewsArticleId (e.g., auto-increment logic)
                    newsArticle.NewsArticleId = (_contextNewsArticle.GetNumberOfArticle() + 1).ToString();

                    // Save the article to the database
                    _contextNewsArticle.SaveNewsArticle(newsArticle);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log the error message
                    Console.WriteLine(ex.Message);
                    return View(newsArticle);  // Return the form with the validation errors
                }
            }

            // If the form submission is invalid, re-populate the dropdown lists
            ViewData["CategoryId"] = new SelectList(_contextCategory.GetCategories(), "CategoryId", "CategoryName", newsArticle.CategoryId);
            ViewBag.CreatedById = new SelectList(_contextAccount.GetAdminAccounts(), "AccountId", "AccountName", newsArticle.CreatedById);
            ViewBag.UpdatedById = new SelectList(_contextAccount.GetAdminAccounts(), "AccountId", "AccountName", newsArticle.UpdatedById);
            return View(newsArticle);
        }

        // GET: NewsArticles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = _contextNewsArticle.GetNewsArticleDetailById(Convert.ToInt32(id));
            if (article == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_contextCategory.GetCategories(), "CategoryId", "CategoryId", article.CategoryId);
            // Lấy danh sách tài khoản để chọn CreatedBy và UpdatedBy
            ViewBag.CreatedById = new SelectList(_contextAccount.GetAdminAccounts(), "AccountId", "AccountName", article.CreatedById);
            ViewBag.UpdatedById = new SelectList(_contextAccount.GetAdminAccounts(), "AccountId", "AccountName", article.UpdatedById);
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
                    // Đặt thông báo thành công vào ViewBag
                    ViewBag.SuccessMessage = "Cập nhật thông tin thành công!";
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
                return View(newsArticle);
            }
            ViewData["CategoryId"] = new SelectList(_contextCategory.GetCategories(), "CategoryId", "CategoryId", newsArticle.CategoryId);
            // Lấy danh sách tài khoản để chọn CreatedBy và UpdatedBy
            ViewBag.CreatedById = new SelectList(_contextAccount.GetAdminAccounts(), "AccountId", "AccountName", newsArticle.CreatedById);
            ViewBag.UpdatedById = new SelectList(_contextAccount.GetAdminAccounts(), "AccountId", "AccountName", newsArticle.UpdatedById);
            return View(newsArticle);
        }

        // GET: NewsArticles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = _contextNewsArticle.GetNewsArticleDetailById(Convert.ToInt32(id));
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
            var newsArticle = _contextNewsArticle.GetNewsArticleDetailById(Convert.ToInt32(id));
            if (newsArticle != null)
            {
                _contextNewsArticle.DeleteNewsArticle(newsArticle);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool NewsArticleExists(int id)
        {
            var tmp = _contextNewsArticle.GetNewsArticleDetailById(Convert.ToInt32(id));
            return (tmp != null) ? true : false;
        }
    }
}
