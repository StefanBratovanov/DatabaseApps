using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkMappings;
using System.Web.Script.Serialization;
using System.IO;

namespace ExportManufacturersAndCamerasAsJson
{
    class ExportManufacturersAndCamerasAsJson
    {
        static void Main()
        {
            var context = new PhotographySystemEntities();

            var manufacturers = context.Manufacturers
                .OrderBy(m => m.Name)
                .Select(m => new
                {
                    manufacturer = m.Name,
                    cameras = m.Cameras.OrderBy(c => c.Model).Select(c => new
                    {
                        model = c.Model,
                        price = c.Price
                    })
                }
                );


            var JSerializer = new JavaScriptSerializer();
            var manufacturersAndModels = JSerializer.Serialize(manufacturers);
            File.WriteAllText("../../manufactureres-and-cameras.json", manufacturersAndModels);
            Console.WriteLine("File manufacturers-and-cameras.json exported.");
        }
    }
}
