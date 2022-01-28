using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Todolist.Models.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Todolist.code;

namespace Todolist.Controllers
{
    public class TodoController : Controller
    {
        Db_TodolistEntities db = new Db_TodolistEntities();

        User_table user = SessionUtil.CreateInstance.UserSession;
        public ActionResult Index()
        {
            var todos = db.Todo_table.Where(m => m.Category_table.user_id == user.id && m.ok == false && m.archived == false).ToList();
            var category = db.Category_table.Where(m => m.user_id == user.id).ToList();
            ViewBag.category = category;
            ViewData["Todos"] = todos;
            return View("Index");
        }

        public ActionResult Category(int id)
        {
            var todos = db.Todo_table.Where(m => m.Category_table.user_id == user.id && m.category_id == id && m.ok == false && m.archived == false).ToList();
            var category = db.Category_table.Where(m=> m.user_id == user.id && m.id == id).SingleOrDefault();
            ViewBag.id = id;
            ViewBag.name = category.name;
            ViewData["categorytodos"] = todos;
            return View();
        }

        //Buraları ajax ile yapsam daha iyi olurdu

        [HttpGet]
        public ActionResult AddFromIndex()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddFromIndex(Todo_table todo)
        {
            var category = db.Category_table.Where(m => m.user_id == user.id && m.id == todo.Category_table.id).FirstOrDefault();
            todo.Category_table = category;
            todo.ok = false;
            todo.archived = false;
            db.Todo_table.Add(todo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddFromCategory()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddFromCategory(Todo_table todo)
        {
            var category = db.Category_table.Where(m => m.user_id == user.id && m.id == todo.Category_table.id).FirstOrDefault();
            todo.Category_table = category;
            todo.ok = false;
            todo.archived = false;
            db.Todo_table.Add(todo);
            db.SaveChanges();
            return RedirectToAction("Category/" + todo.category_id);
        }

        public ActionResult DeleteFromIndex(int id)
        {
            var todo = db.Todo_table.Find(id);
            db.Todo_table.Remove(todo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteFromCategory(int id)
        {
            var todo = db.Todo_table.Find(id);
            int ctgry_id = todo.category_id;
            db.Todo_table.Remove(todo);
            db.SaveChanges();
            return RedirectToAction("Category/" + ctgry_id);
        }

        public ActionResult DeleteFromArchive(int id)
        {
            var todo = db.Todo_table.Find(id);
            int ctgry_id = todo.category_id;
            db.Todo_table.Remove(todo);
            db.SaveChanges();
            return RedirectToAction("Archived");
        }

        public ActionResult DeleteFromOk(int id)
        {
            var todo = db.Todo_table.Find(id);
            int ctgry_id = todo.category_id;
            db.Todo_table.Remove(todo);
            db.SaveChanges();
            return RedirectToAction("Ok");
        }

        public ActionResult ComplateFromIndex(int id)
        {
            var todo = db.Todo_table.Find(id);
            todo.ok = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ComplateFromCategory(int id)
        {
            var todo = db.Todo_table.Find(id);
            todo.ok = true;
            db.SaveChanges();
            return RedirectToAction("Category/" + todo.category_id);
        }
        public ActionResult ToArchiveFromIndex(int id)
        {
            var todo = db.Todo_table.Find(id);
            todo.archived = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ToArchiveFromCategory(int id)
        {
            var todo = db.Todo_table.Find(id);
            todo.archived = true;
            db.SaveChanges();
            return RedirectToAction("Category/" + todo.category_id);
        }

        [HttpGet]
        public ActionResult EditTodoFromIndex()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditTodoFromIndex(Todo_table todo_)
        {
            var oldTodo = db.Todo_table.Find(todo_.id);
            var category = db.Category_table.Where(m => m.user_id == user.id && m.id == todo_.Category_table.id).FirstOrDefault();
            oldTodo.id = todo_.id;
            oldTodo.category_id = category.id;
            oldTodo.name = todo_.name;
            oldTodo.description = todo_.description;
            oldTodo.ticket1 = todo_.ticket1;
            oldTodo.ticket2 = todo_.ticket2;
            oldTodo.ticket3 = todo_.ticket3;
            oldTodo.creation_datetime = todo_.creation_datetime;
            oldTodo.deadline_datetime = todo_.deadline_datetime;
            oldTodo.remember_datetime = todo_.remember_datetime;
            oldTodo.ok = false;
            oldTodo.archived = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditTodoFromCategory(Todo_table todo_)
        {
            var oldTodo = db.Todo_table.Find(todo_.id);
            var category = db.Category_table.Where(m => m.user_id == user.id && m.id == todo_.Category_table.id).FirstOrDefault();
            oldTodo.id = todo_.id;
            oldTodo.category_id = category.id;
            oldTodo.name = todo_.name;
            oldTodo.description = todo_.description;
            oldTodo.ticket1 = todo_.ticket1;
            oldTodo.ticket2 = todo_.ticket2;
            oldTodo.ticket3 = todo_.ticket3;
            oldTodo.creation_datetime = todo_.creation_datetime;
            oldTodo.deadline_datetime = todo_.deadline_datetime;
            oldTodo.remember_datetime = todo_.remember_datetime;
            oldTodo.ok = false;
            oldTodo.archived = false;
            db.SaveChanges();
            return RedirectToAction("Category/"+oldTodo.category_id);
        }

        public ActionResult Uncomplate(int id)
        {
            var todo = db.Todo_table.Find(id);
            todo.ok = false;
            db.SaveChanges();
            return RedirectToAction("Ok");
        }

        public ActionResult Unarchive(int id)
        {
            var todo = db.Todo_table.Find(id);
            todo.archived = false;
            db.SaveChanges();
            return RedirectToAction("Archived");
        }

        public ActionResult Ok()
        {
            var todos = db.Todo_table.Where(m => m.Category_table.user_id == user.id && m.ok == true).ToList();
            return View(todos);
        }

        public ActionResult Archived()
        {
            var todos = db.Todo_table.Where(m => m.Category_table.user_id == user.id && m.archived == true).ToList();
            return View(todos);
        }
    }
}