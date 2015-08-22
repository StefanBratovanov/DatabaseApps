using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkMappings;
using System.Xml.Linq;

namespace ExportPhotographsXML
{
    class ExportPhotographsXML
    {
        static void Main()
        {
            var context = new PhotographySystemEntities();

            var photographsQ = context.Photographs
                .OrderBy(p => p.Title)
                .Select(p => new
                {
                    title = p.Title,
                    Category = p.Category.Name,
                    Link = p.Link,
                    Megapixels = p.Equipment.Camera.Megapixels,
                    CameraModel = p.Equipment.Camera.Manufacturer.Name + " " + p.Equipment.Camera.Model,
                    LensName = p.Equipment.Lens.Manufacturer.Name + " " + p.Equipment.Lens.Model,
                    LensPrice = p.Equipment.Lens.Price
                }).ToList();

            //foreach (var p in photographsQ)
            //{
            //    Console.WriteLine(p.title);
            //    Console.WriteLine(p.Category);
            //    Console.WriteLine(p.Link);
            //    Console.WriteLine(p.Megapixels);
            //    Console.WriteLine(p.CameraModel);
            //    Console.WriteLine(p.LensName);
            //    Console.WriteLine(p.LensPrice);
            //    Console.WriteLine();
            //}

            XElement xmlPhotographs = new XElement("photographs");

            foreach (var p in photographsQ)
            {
                XElement photograph = new XElement("photograph");

                photograph.Add(new XAttribute("title", p.title));
                var category = new XElement("category", p.Category);
                photograph.Add(category);

                photograph.Add(new XElement("link", p.Link));

                var eq = new XElement("equipment");

                eq.Add(new XElement("camera", new XAttribute("megapixels", p.Megapixels), p.CameraModel));

                if (p.LensPrice != null)
                {
                    var lens = new XElement("lens", new XAttribute("price", String.Format("{0:0.00}", p.LensPrice)), p.LensName);
                    eq.Add(lens);
                }
                else
                {
                    var lens = new XElement("lens", p.LensName);
                    eq.Add(lens);
                }

                photograph.Add(eq);

                xmlPhotographs.Add(photograph);
            }

            var xmlDoc = new XDocument(xmlPhotographs);
            xmlDoc.Save("../../photographs.xml");

            Console.WriteLine("Photographs exported to photographs.xml");

        }
    }
}
