namespace DifferentWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 25),
                        LastName = c.String(nullable: false, maxLength: 25),
                        Gender = c.String(nullable: false),
                        PersonalNumber = c.String(nullable: false, maxLength: 10),
                        Birthday = c.DateTime(nullable: false),
                        Country = c.String(nullable: false, maxLength: 25),
                        City = c.String(nullable: false, maxLength: 25),
                        Email = c.String(nullable: false, maxLength: 10),
                        PhoneNo = c.String(nullable: false, maxLength: 25),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: false)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        role = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BranchName = c.String(nullable: false, maxLength: 25),
                        DepartamentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departaments", t => t.DepartamentID, cascadeDelete: true)
                .Index(t => t.DepartamentID);
            
            CreateTable(
                "dbo.Departaments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DepartamentName = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ParentName = c.String(nullable: false, maxLength: 25),
                        ParentLastName = c.String(nullable: false, maxLength: 25),
                        ParentEmail = c.String(nullable: false, maxLength: 25),
                        ParentPhoneNumber = c.String(nullable: false, maxLength: 25),
                        RegistrationDate = c.DateTime(nullable: false),
                        BranchID = c.Int(nullable: false),
                        FirstSemesterID = c.Int(nullable: false),
                        SemesterID = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 25),
                        LastName = c.String(nullable: false, maxLength: 25),
                        Gender = c.String(nullable: false),
                        PersonalNumber = c.String(nullable: false, maxLength: 10),
                        Birthday = c.DateTime(nullable: false),
                        Country = c.String(nullable: false, maxLength: 25),
                        City = c.String(nullable: false, maxLength: 25),
                        Email = c.String(nullable: false, maxLength: 10),
                        PhoneNo = c.String(nullable: false, maxLength: 25),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Branches", t => t.BranchID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: false)
                .ForeignKey("dbo.Semesters", t => t.SemesterID, cascadeDelete: true)
                .Index(t => t.BranchID)
                .Index(t => t.SemesterID)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Gradeings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        Result = c.Double(nullable: false),
                        GradingDate = c.DateTime(nullable: false),
                        ExamSubmition_ID = c.Int(),
                        Subject_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ExamSubmitions", t => t.ExamSubmition_ID)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.Subject_ID)
                .Index(t => t.StudentID)
                .Index(t => t.ExamSubmition_ID)
                .Index(t => t.Subject_ID);
            
            CreateTable(
                "dbo.ExamSubmitions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SubjectID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        DateOfSubmition = c.DateTime(nullable: false),
                        ExamPeriodID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ExamPeriods", t => t.ExamPeriodID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .Index(t => t.SubjectID)
                .Index(t => t.StudentID)
                .Index(t => t.ExamPeriodID);
            
            CreateTable(
                "dbo.ExamPeriods",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PeriodName = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(nullable: false, maxLength: 25),
                        SemestrID = c.Int(nullable: false),
                        BranchID = c.Int(nullable: false),
                        ProfessorID = c.Int(nullable: false),
                        Departament_ID = c.Int(),
                        Semester_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departaments", t => t.Departament_ID)
                .ForeignKey("dbo.Professors", t => t.ProfessorID, cascadeDelete: true)
                .ForeignKey("dbo.Semesters", t => t.Semester_ID)
                .Index(t => t.ProfessorID)
                .Index(t => t.Departament_ID)
                .Index(t => t.Semester_ID);
            
            CreateTable(
                "dbo.Professors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Qualification = c.String(nullable: false, maxLength: 50),
                        RoleID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 25),
                        LastName = c.String(nullable: false, maxLength: 25),
                        Gender = c.String(nullable: false),
                        PersonalNumber = c.String(nullable: false, maxLength: 10),
                        Birthday = c.DateTime(nullable: false),
                        Country = c.String(nullable: false, maxLength: 25),
                        City = c.String(nullable: false, maxLength: 25),
                        Email = c.String(nullable: false, maxLength: 10),
                        PhoneNo = c.String(nullable: false, maxLength: 25),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        semester = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SemesterSubmitions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SemestrID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        SemesterRegistrationDate = c.DateTime(nullable: false),
                        Semester_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Semesters", t => t.Semester_ID)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID)
                .Index(t => t.Semester_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SemesterSubmitions", "StudentID", "dbo.Students");
            DropForeignKey("dbo.SemesterSubmitions", "Semester_ID", "dbo.Semesters");
            DropForeignKey("dbo.Students", "SemesterID", "dbo.Semesters");
            DropForeignKey("dbo.Students", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Gradeings", "Subject_ID", "dbo.Subjects");
            DropForeignKey("dbo.Gradeings", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Gradeings", "ExamSubmition_ID", "dbo.ExamSubmitions");
            DropForeignKey("dbo.ExamSubmitions", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "Semester_ID", "dbo.Semesters");
            DropForeignKey("dbo.Subjects", "ProfessorID", "dbo.Professors");
            DropForeignKey("dbo.Professors", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Subjects", "Departament_ID", "dbo.Departaments");
            DropForeignKey("dbo.ExamSubmitions", "StudentID", "dbo.Students");
            DropForeignKey("dbo.ExamSubmitions", "ExamPeriodID", "dbo.ExamPeriods");
            DropForeignKey("dbo.Students", "BranchID", "dbo.Branches");
            DropForeignKey("dbo.Branches", "DepartamentID", "dbo.Departaments");
            DropForeignKey("dbo.Administrators", "RoleID", "dbo.Roles");
            DropIndex("dbo.SemesterSubmitions", new[] { "Semester_ID" });
            DropIndex("dbo.SemesterSubmitions", new[] { "StudentID" });
            DropIndex("dbo.Professors", new[] { "RoleID" });
            DropIndex("dbo.Subjects", new[] { "Semester_ID" });
            DropIndex("dbo.Subjects", new[] { "Departament_ID" });
            DropIndex("dbo.Subjects", new[] { "ProfessorID" });
            DropIndex("dbo.ExamSubmitions", new[] { "ExamPeriodID" });
            DropIndex("dbo.ExamSubmitions", new[] { "StudentID" });
            DropIndex("dbo.ExamSubmitions", new[] { "SubjectID" });
            DropIndex("dbo.Gradeings", new[] { "Subject_ID" });
            DropIndex("dbo.Gradeings", new[] { "ExamSubmition_ID" });
            DropIndex("dbo.Gradeings", new[] { "StudentID" });
            DropIndex("dbo.Students", new[] { "RoleId" });
            DropIndex("dbo.Students", new[] { "SemesterID" });
            DropIndex("dbo.Students", new[] { "BranchID" });
            DropIndex("dbo.Branches", new[] { "DepartamentID" });
            DropIndex("dbo.Administrators", new[] { "RoleID" });
            DropTable("dbo.SemesterSubmitions");
            DropTable("dbo.Semesters");
            DropTable("dbo.Professors");
            DropTable("dbo.Subjects");
            DropTable("dbo.ExamPeriods");
            DropTable("dbo.ExamSubmitions");
            DropTable("dbo.Gradeings");
            DropTable("dbo.Students");
            DropTable("dbo.Departaments");
            DropTable("dbo.Branches");
            DropTable("dbo.Roles");
            DropTable("dbo.Administrators");
        }
    }
}
