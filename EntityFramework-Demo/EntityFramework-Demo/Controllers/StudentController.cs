using EntityFramework_Demo.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            // ****** Mooved to seeder **********
            //-------------------------------
            //Update Student
            Student  Std = DB.Students.Where(x => x.StudentId == 1).FirstOrDefault();
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
        public ActionResult LazyLoading()
        {
            System.Diagnostics.Debugger.Break();

            //Without Include (Lazy Loaing)
            var DB = new MyContext();
            var students = DB.Students.ToList();
             
            //With Include (Eager Loading)
            var query= DB.Students.Include("Class").Include("Homeworks"); //you can include children by using Include("Class.Teacher")
            var queryText = query.ToString();
            students = query.ToList();


            return View("Index",students);
        }


        public ActionResult EFCache()
        {
            System.Diagnostics.Debugger.Break();

            //Without Include (Lazy Loaing)
            var DB = new MyContext();
            var Std = new Student()
            {
                StudentId=2,
                StudentName = "Elton",
                Age = 18,
                TeachingClassId=1
            };
            var Entities = DB.Set<Student>();
            Entities.Attach(Std);
            DB.Entry(Std).State = EntityState.Modified;
            DB.SaveChanges();

            var students = DB.Students.ToList();
            return View("Index", students);
        }

        public ActionResult TestRepository()
        {
            System.Diagnostics.Debugger.Break();

            var Repo = new Repository<Student, MyContext>();
            //---------
            //Create
            var std = new Student()
            {
                StudentName = "Sarah",
                Age = 25,
                TeachingClassId = 1
            };
            Repo.Create(std);
            //---------
            //Update
            var Std2 = new Student()
            {
                StudentId = 2,
                StudentName = "Elton",
                Age = 18,
                TeachingClassId = 1
            };
            Repo.Update(Std2);

            //---------
            var DB = new MyContext();
            var students = DB.Students.ToList();
            return View("Index", students);
        }
    }
}