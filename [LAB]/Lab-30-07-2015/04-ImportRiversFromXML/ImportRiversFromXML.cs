using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_EntityFrameworkMappings;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace _04_ImportRiversFromXML
{
    class ImportRiversFromXML
    {
        static void Main()
        {
            var context = new GeographyEntities();

            XDocument xmlDoc = XDocument.Load("../../rivers.xml");

            var riverNodes = xmlDoc.XPathSelectElements("/rivers/river");

            foreach (var riverNode in riverNodes)
            {
                string riverName = riverNode.Element("name").Value;
                int riverLength = int.Parse(riverNode.Element("length").Value);
                string riverOutflow = riverNode.Element("outflow").Value;

                int? riverDrainageArea = null;
                if (riverNode.Element("drainage-area") != null)
                {
                    riverDrainageArea = int.Parse(riverNode.Element("drainage-area").Value);
                }

                int? riverAverageDischarge = null;
                if (riverNode.Element("average-discharge") != null)
                {
                    riverAverageDischarge = int.Parse(riverNode.Element("average-discharge").Value);
                }

                //Console.WriteLine("{0} {1} {2} {3} {4}", riverName, riverLength, riverOutflow, riverDrainageArea, riverAverageDischarge);

                var countryNodes = riverNode.XPathSelectElements("countries/country");
                var countries = countryNodes.Select(c => c.Value);
                //Console.WriteLine("{0}->{1}", riverName, string.Join(", ", countries));  

                var river = new River()
                {
                    RiverName = riverName,
                    Length = riverLength,
                    Outflow = riverOutflow,
                    DrainageArea = riverDrainageArea,
                    AverageDischarge = riverAverageDischarge
                };

                context.Rivers.Add(river);

                var countryNames = countryNodes.Select(c => c.Value);

                foreach (var countryName in countryNames)
                {
                    var countryToAddToRiver = context.Countries.FirstOrDefault(c => c.CountryName == countryName);
                    river.Countries.Add(countryToAddToRiver);
                }

                context.SaveChanges();
            }
        }
    }
}
