using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EntityFramework_Demo.DAL
{
    public class MyContext : DbContext
    {
        public MyContext() : base("name=MyConnection")
        {
        }


        public DbSet<Student> Students { get; set; }


    }
}