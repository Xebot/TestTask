﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication2.Models
{
    public class ToDoInitializer : DropCreateDatabaseAlways<ToDoContext>
    {
        //данные для начальной инициализации данных, если в базе было пусто и хочется сразу заполнить ее
        protected override void Seed(ToDoContext db)
        {
            db.ToDos.Add(new ToDo { Id = 1, Task = "Task 1 WebApp" });
            db.ToDos.Add(new ToDo { Id = 2, Task = "Task 2 WebApp" });
            base.Seed(db);
        }
    }
}