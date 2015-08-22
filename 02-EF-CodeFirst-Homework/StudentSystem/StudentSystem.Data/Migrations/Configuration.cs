namespace StudentSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystem.Data.StudentSystemContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "StudentSystem.Data.StudentSystemContext";
        }

        protected override void Seed(StudentSystem.Data.StudentSystemContext context)
        {

            SeedResouces(context);
            SeedStudents(context);

            context.SaveChanges();

            SeedHomeworks(context);
            SeedCourses(context);

        }

        private void SeedCourses(StudentSystemContext context)
        {
            if (context.Courses.Any())
            {
                return;
            }
            var courseBall = new Course
            {
                Name = "Basketball Course",
                Price = 500m,
                StartDate = new DateTime(1990, 5, 5),
                EndDate = new DateTime(1999, 5, 5),
                Description = "Best hoop course",
                Resources = context.Resources.Where(r => r.Name == "Basketball for begginers").ToList(),
                Students = context.Students.Where(s => s.Name != "Jordan").ToList(),
                Homeworks = context.Homeworks.Where(h => h.Content == "DribbleHomework" ||
                                                         h.Content == "ShootingHomework" ||
                                                         h.Content == "DefenceHomework" ||
                                                         h.Content == "SmokinggHomeWork").ToList()
            };
            var courseHacker = new Course
            {
                Name = "Hack Course",
                Price = 5000m,
                StartDate = new DateTime(2015, 5, 5),
                EndDate = new DateTime(2017, 5, 5),
                Description = "Come to our outlaw hack course",
                Resources = context.Resources.Where(r => r.Name != "Basketball for begginers").ToList(),
                Students = context.Students.Where(s => s.Name != "Miroslav").ToList(),
                Homeworks = context.Homeworks.Where(h => h.Content == "DrinkingHomeWork" ||
                                                         h.Content == "SmokinggHomeWork" ||
                                                         h.Content == "BridgeBiddingHomework" ||
                                                         h.Content == "BridgePlayHomework").ToList()
            };


            context.Courses.Add(courseBall);
            context.Courses.Add(courseHacker);

            context.SaveChanges();
        }

        private void SeedHomeworks(StudentSystemContext context)
        {
            if (context.Homeworks.Any())
            {
                return;
            }

            var basketHomeOne = new Homework
            {
                Content = "DribbleHomework",
                ContentType = ContentType.ApplicationPDF,
                SubmissionDate = DateTime.Now,
                Student = context.Students.FirstOrDefault(s => s.Id == 1)
            };

            var basketHomeTwo = new Homework
            {
                Content = "ShootingHomework",
                ContentType = ContentType.ApplicationPDF,
                SubmissionDate = DateTime.Now,
                Student = context.Students.FirstOrDefault(s => s.Id == 2)

            };

            var basketHomeThree = new Homework
            {
                Content = "DefenceHomework",
                ContentType = ContentType.ApplicationZip,
                SubmissionDate = DateTime.Now,
                Student = context.Students.FirstOrDefault(s => s.Id == 3)
            };

            var partyHomeOne = new Homework
            {
                Content = "DrinkingHomeWork",
                ContentType = ContentType.ApplicationZip,
                SubmissionDate = DateTime.Now,
                Student = context.Students.FirstOrDefault(s => s.Id == 1)
            };

            var partyHomeTwo = new Homework
            {
                Content = "SmokinggHomeWork",
                ContentType = ContentType.ApplicationPDF,
                SubmissionDate = DateTime.Now,
                Student = context.Students.FirstOrDefault(s => s.Id == 2)
            };

            var bridgeHomeworkBids = new Homework
            {
                Content = "BridgeBiddingHomework",
                ContentType = ContentType.ApplicationPDF,
                SubmissionDate = DateTime.Now,
                Student = context.Students.FirstOrDefault(s => s.Id == 3)
            };

            var bridgeHomeworkPlays = new Homework
            {
                Content = "BridgePlayHomework",
                ContentType = ContentType.ApplicationPDF,
                SubmissionDate = DateTime.Now,
                Student = context.Students.FirstOrDefault(s => s.Id == 1)
            };

            context.Homeworks.Add(basketHomeOne);
            context.Homeworks.Add(basketHomeTwo);
            context.Homeworks.Add(basketHomeThree);
            context.Homeworks.Add(partyHomeOne);
            context.Homeworks.Add(partyHomeTwo);
            context.Homeworks.Add(bridgeHomeworkBids);
            context.Homeworks.Add(bridgeHomeworkPlays);

            context.SaveChanges();
        }

        private void SeedStudents(StudentSystemContext context)
        {
            if (context.Students.Any())
            {
                return;
            }

            context.Students.Add(new Student
            {
                Name = "Miroslav",
                PhoneNumber = "359 88 555 555",
                RegistrationDate = DateTime.Now,
                Birthday = new DateTime(1981, 4, 11)
            });

            context.Students.Add(new Student
            {
                Name = "Stancho",
                PhoneNumber = "359 88 555 666",
                RegistrationDate = DateTime.Now,
                Birthday = new DateTime(1981, 4, 11)
            });

            context.Students.Add(new Student
            {
                Name = "LongMan",
                PhoneNumber = "359 88 555 999",
                RegistrationDate = DateTime.Now,
                Birthday = new DateTime(1980, 10, 9)
            });
        }

        private void SeedResouces(StudentSystemContext context)
        {
            if (context.Resources.Any())
            {
                return;
            }

            var basketResource = new Resource
            {
                Name = "Basketball for begginers",
                ResourceType = ResourceType.Document,
                URL = "www.learntoplayball.com"
            };

            var bridgeResource = new Resource
            {
                Name = "Bridge for begginers",
                ResourceType = ResourceType.Video,
                URL = "www.bridgebaron.com"
            };

            var partyResource = new Resource
            {
                Name = "PartyHard!!!",
                ResourceType = ResourceType.Video,
                URL = "www.HernanCattaneo.com"
            };

            context.Resources.AddOrUpdate(basketResource);
            context.Resources.AddOrUpdate(bridgeResource);
            context.Resources.AddOrUpdate(partyResource);

            context.SaveChanges();
        }
    }
}
