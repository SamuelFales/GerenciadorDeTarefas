using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Core.Service.Interfaces;
using TaskManager.Web.MVC.App_Start;

namespace TaskManager.Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private readonly IUserService _userService;

        public HomeController()
        {
            _userService = SimpleInjectorConfig.GetInstance<IUserService>();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error(string Message)
        {
            ViewBag.MessageError = Message;
            return View();
        }

    }


}