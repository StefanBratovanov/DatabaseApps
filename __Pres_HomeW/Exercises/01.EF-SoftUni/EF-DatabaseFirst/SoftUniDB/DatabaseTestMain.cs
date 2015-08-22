using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniDB
{
    public class DatabaseTestMain
    {
        static void Main()
        {
             //step 1

            var context = new SoftUniEntities();

            var employeeNames = context.Employees
                .Where(e => e.Salary > 50000)
                .Select(e => e.FirstName);

            var employees =
                from e in context.Employees
                where e.Salary > 50000
                select e.FirstName;

            foreach (var item in employees)
            {
                Console.WriteLine(item);
            }


           // task 3


           //  task 4

            var context = new SoftUniEntities();

            var address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownID = 4
            };

            Employee employee = context.Employees
                .Where(e => e.LastName == "Nakov")
                .FirstOrDefault();

            employee.Address = address;
            context.SaveChanges();

            var empl = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");
            Console.WriteLine(empl.Address.AddressText);

             // Task 5- NO

            var context = new SoftUniEntities();

            var project = context.Projects.Find(2);

            var empProjects = project.Employees.ToList();

            foreach (var empl in empProjects)
            {
                if (empl.Projects.Contains(project))
                {
                    context.Employees.Remove(empl);
                };
            }

           // context.Projects.Remove(project);

            context.SaveChanges();




        }
    }
}
