using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mountains_Code_First
{
    using Mountains_Code_First.Migrations;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    class MountainsCodeFirst
    {
        static void Main()
        {
            var context = new MountainsContext();
            var query = context.Mountains.Select(m => new
            {
                m.Name,
                Countries = m.Countries.Select(c => c.Name),
                Peaks = m.Peaks.Select(p => p.Name).ToList()
            });
            foreach (var m in query)
            {
                Console.WriteLine("{0} ; {1} ; {2}", 
                    m.Name, 
                    String.Join(", ", m.Countries), 
                    String.Join(", ", m.Peaks));
            }
        }
    }
}
