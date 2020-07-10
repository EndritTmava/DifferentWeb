namespace DifferentWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Name = c.String(nullable: false, maxLength: 25),
                        LastName = c.String(nullable: false, maxLength: 25),
                        Gender = c.String(nullable: false),
                        PersonalNumber = c.String(nullable: false, maxLength: 10),
                        Birthday = c.DateTime(nullable: false),
                        Country = c.String(nullable: false, maxLength: 25),
                        City = c.String(nullable: false, maxLength: 25),
                        Email = c.String(nullable: false, maxLength: 50),
                        PhoneNo = c.String(nullable: false, maxLength: 25),
                        Password = c.String(nullable: false),
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
                        AverageGrade = c.Double(nullable: false),
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
                        UserId = c.String(),
                        Name = c.String(nullable: false, maxLength: 25),
                        LastName = c.String(nullable: false, maxLength: 25),
                        Gender = c.String(nullable: false),
                        PersonalNumber = c.String(nullable: false, maxLength: 10),
                        Birthday = c.DateTime(nullable: false),
                        Country = c.String(nullable: false, maxLength: 25),
                        City = c.String(nullable: false, maxLength: 25),
                        Email = c.String(nullable: false, maxLength: 50),
                        PhoneNo = c.String(nullable: false, maxLength: 25),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Branches", t => t.BranchID, cascadeDelete: true)
                .ForeignKey("dbo.Semesters", t => t.SemesterID, cascadeDelete: true)
                .Index(t => t.BranchID)
                .Index(t => t.SemesterID);
            
            CreateTable(
                "dbo.Gradeings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.String(nullable: false),
                        Result = c.Double(nullable: false),
                        GradingDate = c.DateTime(nullable: false),
                        SubjectID = c.Int(nullable: false),
                        ExamSubmition_ID = c.Int(),
                        Student_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ExamSubmitions", t => t.ExamSubmition_ID)
                .ForeignKey("dbo.Students", t => t.Student_ID)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .Index(t => t.SubjectID)
                .Index(t => t.ExamSubmition_ID)
                .Index(t => t.Student_ID);
            
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
                        SemesterID = c.Int(nullable: false),
                        BranchID = c.Int(nullable: false),
                        ProfessorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Branches", t => t.BranchID, cascadeDelete: false)
                .ForeignKey("dbo.Professors", t => t.ProfessorID, cascadeDelete: true)
                .ForeignKey("dbo.Semesters", t => t.SemesterID, cascadeDelete: false)
                .Index(t => t.SemesterID)
                .Index(t => t.BranchID)
                .Index(t => t.ProfessorID);
            
            CreateTable(
                "dbo.Professors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Qualification = c.String(nullable: false, maxLength: 50),
                        UserId = c.String(),
                        Name = c.String(nullable: false, maxLength: 25),
                        LastName = c.String(nullable: false, maxLength: 25),
                        Gender = c.String(nullable: false),
                        PersonalNumber = c.String(nullable: false, maxLength: 10),
                        Birthday = c.DateTime(nullable: false),
                        Country = c.String(nullable: false, maxLength: 25),
                        City = c.String(nullable: false, maxLength: 25),
                        Email = c.String(nullable: false, maxLength: 50),
                        PhoneNo = c.String(nullable: false, maxLength: 25),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        semester = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SemesterSubmitions", "StudentID", "dbo.Students");
            DropForeignKey("dbo.SemesterSubmitions", "Semester_ID", "dbo.Semesters");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Students", "SemesterID", "dbo.Semesters");
            DropForeignKey("dbo.Gradeings", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.Gradeings", "Student_ID", "dbo.Students");
            DropForeignKey("dbo.Gradeings", "ExamSubmition_ID", "dbo.ExamSubmitions");
            DropForeignKey("dbo.ExamSubmitions", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "SemesterID", "dbo.Semesters");
            DropForeignKey("dbo.Subjects", "ProfessorID", "dbo.Professors");
            DropForeignKey("dbo.Subjects", "BranchID", "dbo.Branches");
            DropForeignKey("dbo.ExamSubmitions", "StudentID", "dbo.Students");
            DropForeignKey("dbo.ExamSubmitions", "ExamPeriodID", "dbo.ExamPeriods");
            DropForeignKey("dbo.Students", "BranchID", "dbo.Branches");
            DropForeignKey("dbo.Branches", "DepartamentID", "dbo.Departaments");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SemesterSubmitions", new[] { "Semester_ID" });
            DropIndex("dbo.SemesterSubmitions", new[] { "StudentID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Subjects", new[] { "ProfessorID" });
            DropIndex("dbo.Subjects", new[] { "BranchID" });
            DropIndex("dbo.Subjects", new[] { "SemesterID" });
            DropIndex("dbo.ExamSubmitions", new[] { "ExamPeriodID" });
            DropIndex("dbo.ExamSubmitions", new[] { "StudentID" });
            DropIndex("dbo.ExamSubmitions", new[] { "SubjectID" });
            DropIndex("dbo.Gradeings", new[] { "Student_ID" });
            DropIndex("dbo.Gradeings", new[] { "ExamSubmition_ID" });
            DropIndex("dbo.Gradeings", new[] { "SubjectID" });
            DropIndex("dbo.Students", new[] { "SemesterID" });
            DropIndex("dbo.Students", new[] { "BranchID" });
            DropIndex("dbo.Branches", new[] { "DepartamentID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SemesterSubmitions");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Semesters");
            DropTable("dbo.Professors");
            DropTable("dbo.Subjects");
            DropTable("dbo.ExamPeriods");
            DropTable("dbo.ExamSubmitions");
            DropTable("dbo.Gradeings");
            DropTable("dbo.Students");
            DropTable("dbo.Departaments");
            DropTable("dbo.Branches");
            DropTable("dbo.Administrators");
        }
    }
}
