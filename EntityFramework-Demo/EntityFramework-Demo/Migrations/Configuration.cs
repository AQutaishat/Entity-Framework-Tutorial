namespace EntityFramework_Demo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EntityFramework_Demo.DAL.MyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "EntityFramework_Demo.DAL.MyContext";
        }

        protected override void Seed(EntityFramework_Demo.DAL.MyContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //---------
            //Adding Classes
            var Clss = new TeachingClass() { ClassName = "Class A" };
            context.TeachingClasses.AddOrUpdate(Clss);
            context.TeachingClasses.AddOrUpdate(new TeachingClass() { ClassName = "Class B" });
            context.SaveChanges();
            //---------
            //Adding Students            
            context.Students.Add(new Student() { StudentName = "Anas", Age = 40, TeachingClassId = Clss.Id });

            var Std2 = new Student() { StudentName = "Mohammad", Age = 30, TeachingClassId = Clss.Id };
            context.Students.Add(Std2);
            context.Students.Add(new Student() { StudentName = "Bisher", Age = 30, TeachingClassId = Clss.Id });
            context.SaveChanges();
            //---------
            //Adding Homeworks
            context.Homework.Add(new Homework() { Title = "homework 1", StudentId = Std2.StudentId, });
            context.Homework.Add(new Homework() { Title = "homework 2", StudentId = Std2.StudentId, });
            context.Homework.Add(new Homework() { Title = "homework 3", StudentId = Std2.StudentId, });

            context.SaveChanges();
            //---------
        }
    }
}
