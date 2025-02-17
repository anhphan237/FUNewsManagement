using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;
using FUNewsManagement.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FUNewsManagement.Controllers
{
    public class SystemAccountsController : Controller
    {
        private readonly ISystemAccountService _systemAccountService;

        public SystemAccountsController(ISystemAccountService systemAccountService)
        {
            _systemAccountService = systemAccountService;
        }

        // Hiển thị trang login khi truy cập /SystemAccounts
        public IActionResult Index()
        {
            return View("Login");
        }

        // GET: SystemAccounts/AccountList (hiển thị danh sách tài khoản)
        public async Task<IActionResult> AccountList()
        {
            var myStoreContext = _systemAccountService.GetSystemAccounts();
            return View(myStoreContext.ToList()); // Trả về danh sách tài khoản
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var account = _systemAccountService.GetAccountByEmailAndPassword(email, password);

            if (account != null)
            {
                // Lưu thông tin vào session
                HttpContext.Session.SetInt32("AccountId", account.AccountId);
                HttpContext.Session.SetString("AccountEmail", account.AccountEmail ?? "");
                HttpContext.Session.SetInt32("AccountRole", account.AccountRole ?? 0);
                return RedirectToAction("Index", "NewsArticles");
            }

            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string email, string password, string confirmPassword, int role)
        {
            if (password != confirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match!");
                return View();
            }

            var existingAccount = _systemAccountService.GetAccountByEmail(email);
            if (existingAccount != null)
            {
                ModelState.AddModelError("", "Email is already registered!");
                return View();
            }

            var newAccount = new SystemAccount
            {
                AccountEmail = email,
                AccountPassword = password, // Cần mã hóa mật khẩu trước khi lưu
                AccountRole = role, // Gán role mặc định cho user
                AccountId = (short)(_systemAccountService.GetNumberOfAccount() + 1)
            };

            _systemAccountService.CreateAccount(newAccount);

            return RedirectToAction("Login");
        }

        // GET: NewsArticles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Role = HttpContext.Session.GetInt32("AccountRole");

            var product = _systemAccountService.GetSystemAccountById(Convert.ToInt32(id));
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: SystemAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = _systemAccountService.GetSystemAccountById((short)id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account); // Trả về View hiển thị form chỉnh sửa
        }

        // POST: SystemAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("AccountId,AccountName,AccountEmail,AccountRole,AccountPassword")] SystemAccount systemAccount)
        {
            if (id != systemAccount.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid) // Kiểm tra dữ liệu hợp lệ
            {
                try
                {
                    _systemAccountService.UpdateSystemAccount(systemAccount);
                    // Đặt thông báo thành công vào ViewBag
                    ViewBag.SuccessMessage = "Cập nhật thông tin thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_systemAccountService.GetSystemAccountById((short) id) == null)
                    {
                        return NotFound(); // Nếu tài khoản không tồn tại
                    }
                    else
                    {
                        throw;
                    }
                }
                return View(systemAccount); 
            }

            return View(systemAccount); // Nếu lỗi, quay lại form chỉnh sửa
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa toàn bộ session
            return RedirectToAction("Login", "SystemAccounts"); // Quay về trang Login
        }
    }
}
