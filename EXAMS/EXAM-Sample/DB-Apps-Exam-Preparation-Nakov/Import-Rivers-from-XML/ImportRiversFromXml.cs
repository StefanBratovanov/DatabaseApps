using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Import_Rivers_from_XML
{
    using System.Runtime.InteropServices;
    using System.Runtime.Remoting.Contexts;

    using EF_Mappings;
    using System.Xml.Linq;
    using System.Xml.XPath;

    class ImportRiversFromXml
    {
        static void Main()
        {
            var context = new GeographyEntities();
            var xmlDoc = XDocument.Load("../../rivers.xml");
            var riverElements = xmlDoc.Root.Elements();
            foreach (var riverElement in riverElements)
            {
                var riverEntity = new River();
                riverEntity.RiverName = riverElement.Element("name").Value;
                riverEntity.Length = 
                    int.Parse(riverElement.Element("length").Value);
                riverEntity.Outflow = riverElement.Element("outflow").Value;
                if (riverElement.Element("drainage-area") != null)
                {
                    riverEntity.DrainageArea = 
                        int.Parse(riverElement.Element("drainage-area").Value);
                }
                if (riverElement.Element("average-discharge") != null)
                {
                    riverEntity.AverageDischarge = 
                        int.Parse(riverElement.Element("average-discharge").Value);
                }

                ParseAndAddCountriesToRiver(riverElement, context, riverEntity);

                context.Rivers.Add(riverEntity);
            }

            context.SaveChanges();
        }

        private static void ParseAndAddCountriesToRiver(
            XElement riverElement, GeographyEntities context, River riverEntity)
        {
            var countryElements = riverElement.XPathSelectElements("countries/country");
            foreach (var countryElement in countryElements)
            {
                var countryName = countryElement.Value;
                var countryEntity = context.Countries.FirstOrDefault(c => c.CountryName == countryName);
                if (countryEntity != null)
                {
                    riverEntity.Countries.Add(countryEntity);
                }
                else
                {
                    throw new Exception(string.Format("Cannot find country {0} in the DB", countryName));
                }
            }
        }
    }
}
