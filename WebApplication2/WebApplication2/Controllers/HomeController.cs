using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Web.Routing;

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
        public ActionResult Done(int Id)
        {
            ToDo item = db.ToDos.Find(Id);
            item.isDone = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Add(string Task, string TaskDate)
        {
            DateTime newDate;
            //корректная ли пришла дата (т.к. тип поля Date не во всех браузераз отображается корректно)
            //если строка пустая то просто вернемся на главную страницу
            if ((DateTime.TryParse(TaskDate, out newDate) == true))
            {
                ToDo item = new ToDo { Task = Task, TaskDate = Convert.ToDateTime(TaskDate), isDone = false };
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
            //также покажем дату которая была и если была отметка о выполнении, то позволим её снять
            ViewBag.Task = item.Task;
            ViewBag.TaskDate = item.TaskDate;
            ViewBag.isDone = item.isDone;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(ToDo item, string isDone = null)
        {
            ToDo _item = db.ToDos.Find(item.Id);
            _item.Task = item.Task;
            _item.TaskDate = item.TaskDate;
            //если в чекбокс поставили галочку, то отметим дело выполненым. если её сняли или просто не ставили, то не выполненым
            //по идее отмеченный чекбокс должен был вернуться, как True, а не отмеченный не вернуться вообще, но булевое значение
            //он не возвращал, поэтому нашел такое решение через стринговое значение
            if (isDone != null) { _item.isDone = true; } else { _item.isDone = false; }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        [HttpGet]
        public ActionResult Search(DateTime SearchDate)
        {
            DateTime dt = Convert.ToDateTime(SearchDate);
            IEnumerable<ToDo> todos = db.ToDos;

            IEnumerable<ToDo> _findItems = from t in todos where t.TaskDate.Date == SearchDate select t;
            
            ViewBag.FindItems = _findItems;
            return View();
        }

        
    }
}