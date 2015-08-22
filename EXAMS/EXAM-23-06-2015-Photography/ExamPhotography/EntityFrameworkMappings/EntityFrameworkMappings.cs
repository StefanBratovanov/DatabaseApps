using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkMappings
{
    class EntityFrameworkMappings
    {
        static void Main()
        {
            var context = new PhotographySystemEntities();

            var cameras = context.Cameras
                .OrderBy(c => c.Manufacturer.Name)
                .ThenBy(c => c.Model)
                .Select(c => new
                {
                    c.Model,
                    Manufacturer = c.Manufacturer.Name
                }).ToList();

            foreach (var camera in cameras)
            {
                Console.WriteLine(camera.Manufacturer + " " + camera.Model);
            }
        }
    }
}
