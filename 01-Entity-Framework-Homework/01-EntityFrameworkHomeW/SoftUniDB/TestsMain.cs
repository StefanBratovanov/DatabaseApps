using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniDB
{
    class TestsMain
    {
        static void Main()
        {
            var context = new SoftUniEntities();

            //// Problem 2

            /* Employee michael = new Employee()
             {
                 FirstName = "Michael",
                 LastName = "Jordanov",
                 JobTitle = "playa",
                 DepartmentID = 2,
                 HireDate = DateTime.Now,
                 Salary = 1000000
             };
             DAOClass.Add(michael);

             DAOClass.Delete(michael);*/

            //// Problem 3

            // 3.1
            /*  
            var employees = context.Employees
                        .Where(e => e.Projects.Any(p => p.StartDate >= new DateTime(2001, 1, 1) && p.EndDate < new DateTime(2004, 1, 1)))
                        .Select(e => new
                                {
                                 e.FirstName,
                                 e.LastName,
                                 ManagerName = e.Manager.FirstName + " " + e.Manager.LastName,
                                 Projects = e.Projects.Where(p => p.StartDate >= new DateTime(2001, 1, 1) && p.EndDate < new DateTime(2004, 1, 1))
                                                      .Select(p => new
                                                             {
                                                                 p.Name,
                                                                 p.StartDate,
                                                                 p.EndDate
                                                                })
                                });

             foreach (var e in employees)
             {
                 Console.WriteLine("Employee: {0} {1}, manager: {2}", e.FirstName, e.LastName, e.ManagerName);
                 Console.WriteLine("Projects:");
                 foreach (var p in e.Projects)
                 {
                     Console.WriteLine("{0}: start: {1}, end: {2}", p.Name, p.StartDate, p.EndDate);
                 }
                 Console.WriteLine();
             }  
             */

            // 3.2
            /*
            var addresses = context.Addresses
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Town.Name)
                .Select(a => new
                        {
                            Address = a.AddressText,
                            Town = a.Town.Name,
                            EmployeeCount = a.Employees.Count
                        })
                        .Take(10);


            foreach (var address in addresses)
            {
                Console.WriteLine("{0}, {1} - {2} employees",
                    address.Address,
                    address.Town,
                    address.EmployeeCount
                    );
            }
            */

            // 3.3
            /*
            var employeeById = context.Employees
                .Where(e => e.EmployeeID == 147)
                .Select(e => new
                {   
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    Projets = e.Projects.OrderBy(p => p.Name).Select(p => p.Name)
                });

            foreach (var e in employeeById)
            {
                Console.WriteLine("{0} {1}, {2}", e.FirstName, e.LastName, e.JobTitle);
                Console.WriteLine("Projects:");
                foreach (var p in e.Projets)
                {
                    Console.WriteLine(p);
                }
            }
            */

            // 3.4
            /*
            var departments = context.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .Select(d => new
                {
                    DepartmentName = d.Name,
                    DepartmentManager = d.Manager.LastName,
                    EmployeesCount = d.Employees.Count,
                    Employees = d.Employees
                });

            foreach (var d in departments)
            {
                Console.WriteLine("--{0} - Manager: {1}, Employees: {2}",d.DepartmentName, d.DepartmentManager, d.EmployeesCount);
                Console.WriteLine("Employees:");
                foreach (var e in d.Employees)
                {
                    Console.WriteLine("{0} {1}, hire date: {2}, job: {3}",
                        e.FirstName, e.LastName, e.HireDate, e.JobTitle);
                }
                Console.WriteLine();
            }
            */

            //// Problem 4
           
            //var sw = new Stopwatch();

            //sw.Start();
            //PrintNamesWithNativeQuery(context);
            //Console.WriteLine("Native: {0}", sw.Elapsed);

            //sw.Restart();
            //PrintNamesWithLINQ(context);
            //Console.WriteLine("LINQ: {0}", sw.Elapsed);

            //// Problem 6

            // updated model from database to add the stored procedures

            CallStoredProcedure("Ruth", "Ellerbrock");


        }

        static void CallStoredProcedure(string firstName, string lastName)
        {
            var context = new SoftUniEntities();
            
            var projects = context.usp_GetProjectsByEmployee(firstName, lastName);
            foreach (var p in projects)
            {
                Console.WriteLine(" {0} - {1}, {2}", p.Name, p.Description, p.StartDate);

            }
        }

        private static void PrintNamesWithLINQ(SoftUniEntities context)
        {
            var emps = context.Employees
                .Where(e => e.Projects.Any(p => p.StartDate.Year == 2002))
                .Select(e => new
                {
                    e.FirstName
                });

            foreach (var e in emps)
            {
                Console.WriteLine(e.FirstName);
            }
        }

        private static void PrintNamesWithNativeQuery(SoftUniEntities context)
        {
            string query = "SELECT e.FirstName FROM dbo.Employees e JOIN dbo.EmployeesProjects ep ON ep.EmployeeID = e.EmployeeID JOIN dbo.Projects p ON p.ProjectID = ep.ProjectID GROUP BY e.FirstName, p.StartDate HAVING YEAR(p.StartDate) = 2002";
            var nativeQueryResult = context.Database.SqlQuery<string>(query);
            foreach (var item in nativeQueryResult)
            {
                Console.WriteLine(item);
            }
        }
    }
}
