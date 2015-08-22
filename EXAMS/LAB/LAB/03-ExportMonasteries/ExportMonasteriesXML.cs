using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_DB_First;
using System.Xml;
using System.Xml.Linq;


namespace _03_ExportMonasteries
{
    class ExportMonasteriesXML
    {
        static void Main()
        {
            var context = new GeographyEntities();

            var countryMon = context.Countries
                .OrderBy(c => c.CountryName)
                .Where(c => c.Monasteries.Any())
                .Select(c => new
                {
                    c.CountryName,
                    Monasteries = c.Monasteries.OrderBy(m => m.Name).Select(m => m.Name)
                })
                .ToList();

            //foreach (var item in countryMon)
            //{
            //    Console.WriteLine(item.CountryName + "; " + string.Join(", ", item.Monasteries));
            //}

            XElement xmlMonasteries = new XElement("monasteries");

            foreach (var country in countryMon)
            {
                var xmlCountry = new XElement("country");
                xmlCountry.Add(new XAttribute("name", country.CountryName));

                foreach (var monastrery in country.Monasteries)
                {
                    xmlCountry.Add(new XElement("monastery", monastrery));
                }

                xmlMonasteries.Add(xmlCountry);
            }

            var xmlDoc = new XDocument(xmlMonasteries);
            xmlDoc.Save("../../countryMon.xml");
        }
    }
}
