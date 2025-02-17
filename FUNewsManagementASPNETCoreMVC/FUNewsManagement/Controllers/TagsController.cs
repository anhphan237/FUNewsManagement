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
    public class TagsController : Controller
    {
        private readonly ITagService _contextTag;

        public TagsController(ITagService contextTag)
        {
            _contextTag = contextTag;
        }

        // GET: Tags
        public async Task<IActionResult> Index()
        {
            return View(_contextTag.GetTags());
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _contextTag.GetTagById(id.ToString());
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TagId,TagName,Note")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                _contextTag.SaveTag(tag);
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _contextTag.GetTagById(id.ToString());
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        // POST: Tags/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TagId,TagName,Note")] Tag tag)
        {
            if (id != tag.TagId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contextTag.UpdateTag(tag);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.TagId))
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
            return View(tag);
        }

        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = _contextTag?.GetTagById(id.ToString());   
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tag = _contextTag.GetTagById(id.ToString());
            if (tag != null)
            {
                _contextTag.DeleteTag(tag);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TagExists(int id)
        {
            var tmp = _contextTag.GetTagById(id.ToString());
            return (tmp != null) ? true : false;
        }
    }
}
