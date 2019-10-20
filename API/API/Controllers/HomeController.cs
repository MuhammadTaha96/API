using API.Operations;
using API.ViewModels;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class HomeController : Controller
    {
        SmartLibraryContext db = new SmartLibraryContext();

        public ActionResult Index()
        {
            string test = string.Empty;
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginViewModel user)
        {
            if (Account.ValidateUser(user))
            {
                return RedirectToActionPermanent("Index");
            }
            else
            {
                ViewData["LoginFailureMessage"] = "Username or Password are incorrect";
            }

            return View();
        }




    }
}
