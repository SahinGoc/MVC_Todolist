using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Todolist.code;
using Todolist.Models.Entity;

namespace Todolist.Controllers
{   [AllowAnonymous]
    public class UserController : Controller
    {
        
        Db_TodolistEntities db = new Db_TodolistEntities();

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();   
        }

        [HttpPost]
        public ActionResult SignIn(User_table user)
        {
            var signIn = db.User_table.Where(m => m.email == user.email && m.password == user.password).SingleOrDefault();
            if(signIn == null)
            {
                ViewBag.error = "Hatalı giriş";
                return View();
                
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.name_surname, false);
                SessionUtil.CreateInstance.UserSession = signIn;
                return RedirectToAction("Index", "Todo");
            }
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User_table user)
        {
            db.User_table.Add(user);
            try
            {
                db.SaveChanges();
                return View("SignIn");
            }
            catch (DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }
            return View("SignIn");
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            SessionUtil.CreateInstance.UserSession = null;
            return RedirectToAction("SignIn");
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditProfile(User_table user_)
        {
            var oldUser = db.User_table.Find(user_.id);
            oldUser.id = user_.id;
            oldUser.name_surname = user_.name_surname;
            oldUser.email = user_.email;
            oldUser.password = user_.password;
            db.SaveChanges();
            SessionUtil.CreateInstance.UserSession = oldUser;
            return View();
        }
    }
}
