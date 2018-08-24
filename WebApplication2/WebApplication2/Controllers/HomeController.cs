using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        ToDoContext db = new ToDoContext();


        public ActionResult Index()
        {
            //получаем все объекты ToDo
            IEnumerable<ToDo> todos = db.ToDos;
            ViewBag.Todos = todos;
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            ToDo item = db.ToDos.Find(Id);
            db.ToDos.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Add(string Task)
        {
            if (Task != "") //проверим не пустая ли строка пришла, чтобы не заполнять пустыми данными
                            //если строка пустая то просто вернемся на главную страницу
            {
                ToDo item = new ToDo { Task = Task };
                db.ToDos.Add(item);
                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            ViewBag.Id = Id;
            //найдем этот элемент
            ToDo item = db.ToDos.Find(Id);
            //запишем данные поля "Task", чтобы вывести эти данные на форме редактирования
            //и пользователю не пришлось заново все вводить, если он хочет внести небольшую правку
            ViewBag.Task = item.Task;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ToDo item)
        {
            ToDo _item = db.ToDos.Find(item.Id);
            _item.Task = item.Task;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}