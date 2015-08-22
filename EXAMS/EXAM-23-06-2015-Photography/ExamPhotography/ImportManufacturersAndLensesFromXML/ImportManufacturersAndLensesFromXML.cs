using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkMappings;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ImportManufacturersAndLensesFromXML
{
    class ImportManufacturersAndLensesFromXML
    {
        static void Main()
        {
            var context = new PhotographySystemEntities();
            XDocument xmlDoc = XDocument.Load("../../manufacturers-and-lenses.xml");

            var xManufacturers = xmlDoc.XPathSelectElements("/manufacturers-and-lenses/manufacturer");
            int manufacturerNumber = 1;

            foreach (var xManufacturer in xManufacturers)
            {
                Console.WriteLine("Processing manufacturer #{0} ...", manufacturerNumber++);
                Manufacturer manufacturer = CreateManufacturerIfNotExists(context, xManufacturer);

                var xLenses = xManufacturer.XPathSelectElements("lenses/lens");
                CreateLensesIfNotExist(context, xLenses, manufacturer);
                Console.WriteLine();
            }
        }

        private static void CreateLensesIfNotExist(PhotographySystemEntities context, IEnumerable<XElement> xLenses, Manufacturer manufacturer)
        {
            foreach (var xLense in xLenses)
            {
                var lensModel = xLense.Attribute("model").Value;
                var lensType = xLense.Attribute("type").Value;
                var lensPrice = xLense.Attribute("price");

                var lens = context.Lenses.FirstOrDefault(l => l.Model == lensModel);

                if (lens != null)
                {
                    Console.WriteLine("Existing lens: {0}", lensModel);
                }

                else
                {
                    lens = new Lens
                    {
                        Model = lensModel,
                        Type = lensType,
                        Price = lensPrice != null ? decimal.Parse(lensPrice.Value) : default(decimal?),
                        ManufacturerId = manufacturer.Id
                    };

                    context.Lenses.Add(lens);
                    context.SaveChanges();
                    Console.WriteLine("Created lens: {0}", lensModel);
                }

            }
        }

        private static Manufacturer CreateManufacturerIfNotExists(PhotographySystemEntities context, XElement xManufacturer)
        {
            Manufacturer manufacturer = null;
            var xManifName = xManufacturer.Element("manufacturer-name");

            if (xManifName != null)
            {
                var manifacName = xManifName.Value;

                manufacturer = context.Manufacturers.FirstOrDefault(m => m.Name == manifacName);
                if (manufacturer != null)
                {
                    Console.WriteLine("Existing manufacturer: {0}", manifacName);
                }
                else
                {
                    manufacturer = new Manufacturer
                    {
                        Name = manifacName
                    };

                    context.Manufacturers.Add(manufacturer);
                    context.SaveChanges();
                    Console.WriteLine("Created manufacturer: {0}", manifacName);
                }
            }
            return manufacturer;
        }
    }
}
