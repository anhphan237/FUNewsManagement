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
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _contextCategory;

        public CategoriesController(ICategoryService contextCategory)
        {
            _contextCategory = contextCategory;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = _contextCategory.GetCategories();
            return View(categories);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _contextCategory.GetCategoryById(id ?? 0);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            ViewData["ParentCategoryId"] = new SelectList(_contextCategory.GetCategories(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDesciption,ParentCategoryId,IsActive")] Category category)
        {
            if (ModelState.IsValid)
            {
                _contextCategory.SaveCategory(category);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentCategoryId"] = new SelectList(_contextCategory.GetCategories(), "CategoryId", "CategoryName", category.ParentCategoryId);
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _contextCategory?.GetCategoryById(id ?? 0);  
            if (category == null)
            {
                return NotFound();
            }
            ViewData["ParentCategoryId"] = new SelectList(_contextCategory?.GetCategories(), "CategoryId", "CategoryName", category.ParentCategoryId);
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("CategoryId,CategoryName,CategoryDesciption,ParentCategoryId,IsActive")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contextCategory.UpdateCategory(category);  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            ViewData["ParentCategoryId"] = new SelectList(_contextCategory?.GetCategories(), "CategoryId", "CategoryName", category.ParentCategoryId);
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _contextCategory?.GetCategoryById(Convert.ToInt32(id));
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var category = _contextCategory?.GetCategoryById(Convert.ToInt32(id));
            if (category != null)
            {
                _contextCategory?.DeleteCategory(category);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(short id)
        {
            var tmp = _contextCategory?.GetCategoryById(Convert.ToInt32(id));
            return (tmp != null) ? true : false;
        }
    }
}
