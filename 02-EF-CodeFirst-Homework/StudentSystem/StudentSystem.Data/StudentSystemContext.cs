namespace StudentSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;
    using Data.Migrations;


    public class StudentSystemContext : DbContext
    {

        public StudentSystemContext()
            : base("name=StudentSystemContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemContext, Configuration>());
        }

        public IDbSet<Student> Students { get; set; }
        public IDbSet<Course> Courses { get; set; }
        public IDbSet<Resource> Resources { get; set; }
        public IDbSet<Homework> Homeworks { get; set; }

    }
}