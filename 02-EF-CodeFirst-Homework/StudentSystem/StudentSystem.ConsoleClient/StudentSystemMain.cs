using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentSystem.Models;
using StudentSystem.Data;

namespace StudentSystem.ConsoleClient
{
    class StudentSystemMain
    {
        static void Main()
        {
            var context = new StudentSystemContext();
            var studentsConut = context.Students.Count();

            // Problem 3

            /* 3.1. Lists all students and their homework submissions. Select only their names and for each homework - content and 
                    content-type */

            var stHom = context.Homeworks.Where(h => h.StudentId == 1);

            foreach (var h in stHom)
            {
                Console.WriteLine(h.Content);
            }
            /*
            var studentsHomeworks =
                context.Students.Select(s => new
                                               {
                                                   s.Name,
                                                   Homeworks = s.Courses.Select(c => c.Homeworks.Select(h => new
                                                                                  {
                                                                                      h.Content,
                                                                                      h.ContentType
                                                                                  }))
                                               });

            foreach (var sh in studentsHomeworks)
            {
                Console.WriteLine("{0}, Homeworks: ", sh.Name);
                Console.WriteLine(new string('-', 25));

                foreach (var course in sh.Homeworks)
                {
                    foreach (var h in course)
                    {
                        Console.WriteLine("Content: {0}; Content Type: {1}", h.Content, h.ContentType);
                    }
                }
                Console.WriteLine();
            }
            */

            /* 3.2. List all courses with their corresponding resources. Select the course name and description and everything for 
                    each resource. Order the courses by start date (ascending), then by end date (descending)  */

            /*
            var courses = context.Courses
                        .OrderBy(c=> c.StartDate)
                        .ThenByDescending(c => c.EndDate)
                        .Select(c => new
                        {
                            c.Name,
                            c.Description,
                            Resources = c.Resources.Select(r => new
                            {
                                r.Name,
                                r.ResourceType,
                                r.URL
                            })
                        });

            foreach (var c in courses)
            {
                Console.WriteLine("Course: {0}; Description: {1}", c.Name, c.Description);
                Console.WriteLine("Resources:");
                foreach (var r in c.Resources)
                {
                    Console.WriteLine("{0}, Type: {1}", r.Name, r.ResourceType);
                    Console.WriteLine("Link: {0}", r.URL);
                }
                Console.WriteLine(new string('-', 25));
                Console.WriteLine();
            }
            */

            /* 3.3. List all courses with more than 5 resources. Order them by resources count (descending), 
                    then by start date (descending). Select only the course name and the resource count */

            /*
            var bigCourses = context.Courses
                                    .Where(c => c.Resources.Count > 5)
                                    .OrderByDescending(c => c.Resources.Count)
                                    .ThenByDescending(c => c.StartDate)
                                    .Select(c => new
                                    {
                                        c.Name,
                                        ResourcesCount = c.Resources.Count
                                    });

            foreach (var c in bigCourses)
            {
                Console.WriteLine("Course: {0}, ResorsesCount = {1}", c.Name, c.ResourcesCount);
            }
            */

            /* 3.4.	List all courses which were active on a given date (choose the date depending on the data seeded to ensure there
                    are results), and for each course count the number of students enrolled. Select the course name, start and end date, 
                    course duration (difference between end and start date) and number of students enrolled.
                    Order the results by the number of students enrolled (in descending order), then by duration (descending).  */
            /*
            var activeCourses = context.Courses
                                       .Where(c => c.StartDate < DateTime.Now && c.EndDate > DateTime.Now)
                                       .ToList()
                                       .OrderByDescending(c => c.Students.Count)
                                       .ThenByDescending(c => (c.EndDate - c.StartDate).TotalDays)
                                       .Select(c => new
                                       {
                                           c.Name,
                                           c.StartDate,
                                           c.EndDate,
                                           Duration = (c.EndDate - c.StartDate).TotalDays,
                                           StudentsCount = c.Students.Count
                                       });

            foreach (var c in activeCourses)
            {
                Console.WriteLine("Active Course: {0}, Start: {1}, End: {2}", c.Name, c.StartDate, c.EndDate);
                Console.WriteLine("Duration: {0} days, Students Count: {1}", c.Duration, c.StudentsCount);
                Console.WriteLine();
            }
            */

            /* 3.5. For each student, calculate the number of courses she’s enrolled in, the total price of these courses and the
                    average price per course for the student. Select the student name, number of courses, total price and average price.
                    Order the results by total price (descending), then by number of courses (descending) and 
                    then by the student’s name (ascending). */

          /*
            var allStudents = context.Students
                                     .OrderByDescending(s => s.Courses.Sum(c => c.Price))
                                     .ThenByDescending(s => s.Courses.Count)
                                     .ThenBy(s => s.Name)
                                     .Select(s => new
                                     {
                                         s.Name,
                                         CoursesCount = s.Courses.Count,
                                         TotalPrice = s.Courses.Sum(c => c.Price),
                                         AvgPrice = s.Courses.Sum(c => c.Price) / s.Courses.Count
                                         //AveragePrice = s.Courses.Average(c => c.Price)
                                     });

            foreach (var s in allStudents)
            {
                Console.WriteLine("Student: {0}, Courses enrolled in: {1}", s.Name, s.CoursesCount);
                Console.WriteLine("Total price: {0:f2} lv., Average price: {1:f2} lv.", s.TotalPrice, s.AvgPrice);
                Console.WriteLine();
            }
           */
            var start = DateTime.Parse("01.12.2015");
            var end = DateTime.Parse("30-Jan-2015");
        
           // Console.WriteLine(start);
            Console.WriteLine(start);
        }
    }
}
