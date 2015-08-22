using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_EntityFrameworkMappings
{
    class EntityFrameworkMappings
    {
        static void Main()
        {
            var context = new GeographyEntities();

            var continents = context.Continents.Select(c => c.ContinentName);

            foreach (var continent in continents)
            {
                Console.WriteLine(continent);
            }
        }
    }
}
