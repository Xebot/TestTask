using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public DateTime TaskDate { get; set; }
        public bool isDone { get; set; }
    }

    
}