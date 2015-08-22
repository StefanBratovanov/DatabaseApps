using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniDB
{
    class DAOClass
    {
        public static void Add(Employee emlpoyee)
        {
            var context = new SoftUniEntities();

            context.Employees.Add(emlpoyee);
            context.SaveChanges();
        }

        public static Employee FindByKey(object key)
        {
            var context = new SoftUniEntities();

            Employee emp = context.Employees.Find(key);
            return emp;
        }

        public static void Modify(Employee employee)
        {
            var context = new SoftUniEntities();
            
        }

        public static void Delete(Employee employee)
        {
            var context = new SoftUniEntities();

            context.Employees.Remove(employee);
            context.SaveChanges();
        }
    }
}
