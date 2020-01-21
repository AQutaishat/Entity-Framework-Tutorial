namespace EntityFramework_Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Homework",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StudentMaster", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.StudentMaster",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentName = c.String(nullable: false, maxLength: 500, fixedLength: true),
                        Age = c.Int(nullable: false),
                        TeachingClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.TeachingClasses", t => t.TeachingClassId, cascadeDelete: true)
                .Index(t => t.TeachingClassId);
            
            CreateTable(
                "dbo.TeachingClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Homework", "StudentId", "dbo.StudentMaster");
            DropForeignKey("dbo.StudentMaster", "TeachingClassId", "dbo.TeachingClasses");
            DropIndex("dbo.StudentMaster", new[] { "TeachingClassId" });
            DropIndex("dbo.Homework", new[] { "StudentId" });
            DropTable("dbo.TeachingClasses");
            DropTable("dbo.StudentMaster");
            DropTable("dbo.Homework");
        }
    }
}
