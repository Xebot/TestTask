using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TestDB
{
    class Program
    {
        //таблица в БД имеет такую же структуру. Поле ID у которого тип int, оно является праймарики и не может быть нулл и поле Task которое имеет тип nvarchar(max)
        class ToDo
        {
            public int Id { get; set; }
            public string Task { get; set; }
        }

        class ToDoContext : DbContext
        {
            public ToDoContext() : base("DbConnection") { }
            public DbSet<ToDo> ToDos { get; set; }
        }
        
        //класс который будет выполнять все наши действия из меню
        class TaskList
        {
            public ToDo AddNewItem()
            {
                Console.WriteLine("Введите новое задание:");
                string _task = Console.ReadLine();
                ToDo Item = new ToDo { Task = _task };
                return Item;
            } 
            public void ShowAllItems(DbSet<ToDo> _todos)
            {
                Console.WriteLine("Список дел");
                foreach (ToDo t in _todos)
                {
                    Console.WriteLine("Id={0}, Task={1}", t.Id, t.Task);
                }
                Console.WriteLine("Конец списка. Нажмите любую клавишу...");
                Console.ReadLine();
            }
            public ToDo DeleteItem(DbSet<ToDo> _todos)
            {
                ShowAllItems(_todos);
                while (true)
                {
                    Console.WriteLine("Введите Id задания которое хотите удалить");
                    int i = 0;
                    try { i = Convert.ToInt32(Console.ReadLine()); }
                    catch (Exception ex) { Console.WriteLine("Ошибка: " + ex.Message); Console.ReadLine(); }
                    if (i != 0)
                    {
                        ToDo item = _todos.Find(i);
                        if (item!=null)
                                              return item;
                    }
                }
                
            }
            public ToDo EditItem (DbSet<ToDo> _todos)
            {
                int i = 0;
                //ждем пока пользователь введет ID записи которую хочет отредактировать. если такой записи нет
                // то он сможет выбрать нужную(если ошибся до этого при вводе). Если он передумал то может выйти нажав 0
                while (true)
                    {
                    Console.WriteLine("Введите Id записи которую хотите отредактировать (для выхода нажмите 0)");
                    try
                    {
                        i = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка :" + ex.Message);
                        Console.ReadLine();
                    }
                    if (i != 0)
                    {

                        ToDo item = _todos.Find(i);
                        if (item != null)
                        {
                            Console.WriteLine("Введите правильное задание");
                            string _task = Console.ReadLine();
                            item.Task = _task;
                            return item;
                        }
                        else Console.WriteLine("Записи с таким Id не найдено");
                    }
                    else return null;
                 
                    }               
            }
        }
        static void Main(string[] args)
        {

            int choise=0;
            
            TaskList tasklist = new TaskList();
            using (ToDoContext db = new ToDoContext())
            {
                //получаем объекты из БД
                var todos = db.ToDos;
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Добро пожаловать в ваш список дел. Что вы хотите сделать? \n 1. Посмотреть список дел \n 2. Добавить новое задание \n 3. Удалить имеющееся задание \n 4. Отредактировать одно из заданий \n 5. Выход");
                    Console.WriteLine("Ваш выбор:");
                    try
                    {
                        choise = Convert.ToInt32(Console.ReadLine());
                    }
                    catch(Exception ex)
                    { Console.WriteLine("Ошибка: " + ex.Message); }

                    switch (choise)
                    {
                        case 1:
                            tasklist.ShowAllItems(todos);
                            break;
                        case 2:
                            db.ToDos.Add(tasklist.AddNewItem());
                            db.SaveChanges();
                            break;
                        case 3:
                            db.ToDos.Remove(tasklist.DeleteItem(todos));
                            db.SaveChanges();
                            break;
                        case 4:
                            if (tasklist.EditItem(todos)!=null) db.SaveChanges();
                            break;
                        case 5:
                            return;
                        default:
                            Console.WriteLine("Надо сделать выбор! Для продолжения нажимте любую клавишу...");
                            Console.ReadLine();
                            break;
                    }
                }
            }


            //using (ToDoContext db = new ToDoContext())
            //{
            //    //ToDo task1 = new ToDo { Id = 2, Task = "Task number two" };

            //    db.ToDos.Add(task1);
            //    db.SaveChanges();
            //    Console.WriteLine("All done");

            //    var todos = db.ToDos;
            //    Console.WriteLine("Список объектов");
            //    foreach(ToDo t in todos)
            //    {
            //        Console.WriteLine("Id={0}, Task={1}", t.Id,t.Task);
            //    }
            //}
            //Console.ReadLine();
        }
    }
}
