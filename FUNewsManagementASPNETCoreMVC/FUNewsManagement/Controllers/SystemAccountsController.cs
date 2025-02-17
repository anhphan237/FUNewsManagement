﻿using System;
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

        /*public IActionResult Details(int id)
        {
            var account = _systemAccountService.GetSystemAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }*/

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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa toàn bộ session
            return RedirectToAction("Login", "SystemAccounts"); // Quay về trang Login
        }
    }
}
