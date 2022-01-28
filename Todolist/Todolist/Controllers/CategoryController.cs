using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todolist.code;
using Todolist.Models.Entity;

namespace Todolist.Controllers
{
    public class CategoryController : Controller
    {
        Db_TodolistEntities db = new Db_TodolistEntities();
        User_table user = SessionUtil.CreateInstance.UserSession;
        public ActionResult AddNewCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewCategory(Category_table category)
        {
            db.Category_table.Add(category);
            db.SaveChanges();
            return RedirectToAction("Category/" + category.id, "Todo");
        }

        public ActionResult CategoryList()
        {
            var category = db.Category_table.Where(m => m.user_id == user.id).ToList();
            return PartialView("CategoryList", category);
        }

        public ActionResult DeleteCategory(int id)
        {
            var todos = db.Todo_table.Where(m => m.Category_table.user_id == user.id && m.category_id == id && m.ok == false && m.archived == false).ToList();
            foreach(var todo in todos)
            {
                db.Todo_table.Remove(todo);
            }
            db.SaveChanges();
            var category = db.Category_table.Find(id);
            db.Category_table.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index", "Todo");
        }
    }
}