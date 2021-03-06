﻿using Encrypt;
using Logger;
using ModuleCore.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private IEncryptHelper encrypt;
        private IMyLogger myLogger;
        public HomeController(IEncryptHelper encrypt, IIocContainer iocContainer)
        {
            myLogger = iocContainer.GetInstance<IMyLogger>();
            this.encrypt = encrypt;
        }
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Logger = myLogger?.ToString();
            ViewBag.Encrypt = encrypt?.ToString();
            var dd = encrypt.EncryptData("ssss");
            ViewBag.Data = dd;
            return View();
        }
    }
}