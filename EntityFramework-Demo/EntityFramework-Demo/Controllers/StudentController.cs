using EntityFramework_Demo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityFramework_Demo.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            System.Diagnostics.Debugger.Break();
            //-------------------------------
            //Create Database
            var DB = new MyContext();
            var Students = DB.Students.ToList();

            //-------------------------------
            //Add Student
            Student Std;
            if (Students.Count == 0)
            {
                Std = new Student() { StudentName = "Anas", Age = 40 };
                DB.Students.Add(Std);

                var Std2 = new Student() { StudentName = "Mohammad", Age = 30 };
                DB.Students.Add(Std2);

                var Std3 = new Student() { StudentName = "Bisher", Age = 30 };
                DB.Students.Add(Std3);

                DB.SaveChanges();
                Students = DB.Students.ToList();
            }
            //-------------------------------
            //Update Student
            Std = DB.Students.Where(x => x.StudentId == 1).FirstOrDefault();
            if (Std != null)
            {
                Std.StudentName = "Updated";
                //DB.Students.Update(std); //Wrong
                DB.SaveChanges();
            }

            //-------------------------------
            //Remove Student
            Std = DB.Students.Where(x => x.StudentId == 1).FirstOrDefault();
            if (Std != null)
            {
                DB.Students.Remove(Std);
                DB.SaveChanges();
            }
            //-------------------------------
            //First vs Single
            Std = DB.Students.Where(x => x.Age == 30).First();
            Std = DB.Students.First(x => x.Age == 30);
            try
            {
                Std = DB.Students.Single(x => x.Age == 30);
            }
            catch (Exception e)
            {

            }
            Std = DB.Students.FirstOrDefault(x => x.Age == 20);
            Std = DB.Students.SingleOrDefault(x => x.Age == 20);


            //-------------------------------
            return View(Students);
        }

        public ActionResult IQuerableVSIEnumarable()
        {
            System.Diagnostics.Debugger.Break();
            //-------------------------------
            //IQuerable
            var DB = new MyContext();
            var query = DB.Students.Where(x => x.Age < 40);
            var QueryText = query.ToString();

            query = query.Where(x => x.StudentName.Contains("h"));
            QueryText = query.ToString();

            //-------------------------------
            //IEnumrable 
            var List = query.ToList();
            var Count = List.Count();
            //-------------------------------
            //Implicit calling ToList
            foreach (var item in query)
            {
                //do something
            }
            //-------------------------------

            return View(List);
        }

    }
}