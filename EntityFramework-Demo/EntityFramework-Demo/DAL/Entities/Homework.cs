using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFramework_Demo
{
    public class Homework
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Title { get; set; }
    }
}