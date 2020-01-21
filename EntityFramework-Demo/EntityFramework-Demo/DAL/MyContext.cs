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

            //Database.SetInitializer<MyContext>(new DropCreateDatabaseIfModelChanges<MyContext>());

            //this.Configuration.LazyLoadingEnabled = false;

            //Database.SetInitializer<MyContext>(new CreateDatabaseIfNotExists<MyContext>()); //Default
            //Database.SetInitializer<MyContext>(new DropCreateDatabaseAlways<MyContext>());
            //Database.SetInitializer<MyContext>(new SchoolDBInitializer());
        }


        public DbSet<Student> Students { get; set; }
        public DbSet<TeachingClass> TeachingClasses { get; set; }
        public DbSet<Homework> Homework { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Set StudentName column size to 50
            modelBuilder.Entity<Student>()
                    .Property(p => p.StudentName)
                    .HasMaxLength(50);

            //Set StudentName column size to 50 and change datatype to nchar 
            //IsFixedLength() change datatype from nvarchar to nchar
            modelBuilder.Entity<Student>()
                    .Property(p => p.StudentName)
                    .HasMaxLength(500)
                    .IsFixedLength();

            //Set size decimal(2,2)
            modelBuilder.Entity<Student>()
                .HasKey<int>(s => s.StudentId)
                .ToTable("StudentMaster");
        }
    }
}