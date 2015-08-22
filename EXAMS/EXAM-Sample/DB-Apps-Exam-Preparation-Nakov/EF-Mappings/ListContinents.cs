using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Mappings
{
    class ListContinents
    {
        static void Main()
        {
            var context = new GeographyEntities();
            foreach (var c in context.Continents)
            {
                Console.WriteLine(c.ContinentName);
            }
        }
    }
}
