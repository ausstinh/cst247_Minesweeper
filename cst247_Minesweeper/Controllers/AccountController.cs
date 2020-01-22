﻿using cst247_Minesweeper.Models;
using cst247_Minesweeper.Models.cst247_Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cst247_Minesweeper.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            AccountBusinessService bs = new AccountBusinessService();

            if (bs.Authenticate(user))
            {
                return View("LoginSuccess", user);
            }
            else
            {
                return View("LoginFailed");
            }
        }

        [HttpPost]
        public ActionResult Register(UserModel user)
        {
            AccountBusinessService bs = new AccountBusinessService();

            if (bs.Register(user))
            {
                return View("Login", user);
            }
            else
            {
                return View("RegisterFailed");
            }
        }
    }
}