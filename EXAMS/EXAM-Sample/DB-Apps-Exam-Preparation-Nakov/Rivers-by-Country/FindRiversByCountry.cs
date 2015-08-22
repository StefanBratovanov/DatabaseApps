using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rivers_by_Country
{
    using System.Xml.Linq;
    using System.Xml.XPath;

    using EF_Mappings;

    class FindRiversByCountry
    {
        static void Main()
        {
            var context = new GeographyEntities();
            var xmlDocInput = XDocument.Load("../../rivers-query.xml");
            var queryResults = new XElement("results");
            foreach (var queryElement in xmlDocInput.XPathSelectElements("/queries/query"))
            {
                var riversQuery = BuildRiversQuery(context, queryElement);
                var riversElement = new XElement("rivers");
                riversElement.Add(new XAttribute("total-count", riversQuery.Count().ToString()));
                var maxResultsAttribute = queryElement.Attribute("max-results");
                if (maxResultsAttribute != null)
                {
                    int maxResults = int.Parse(maxResultsAttribute.Value);
                    riversQuery = riversQuery.Take(maxResults);
                }
                var riverNames = riversQuery.Select(r => r.RiverName).ToList();
                foreach (var riverName in riverNames)
                {
                    riversElement.Add(new XElement("river", riverName));
                }
                riversElement.Add(new XAttribute("listed-count", riversQuery.Count().ToString()));
                queryResults.Add(riversElement);
            }

            Console.WriteLine(queryResults);
        }

        private static IQueryable<River> BuildRiversQuery(
            GeographyEntities context, XElement queryElement)
        {
            IQueryable<River> riversQuery = context.Rivers.AsQueryable();
            foreach (var countryElement in queryElement.XPathSelectElements("country"))
            {
                var countryName = countryElement.Value;
                riversQuery = riversQuery.Where(
                    r => r.Countries.Any(c => c.CountryName == countryName));
            }
            riversQuery = riversQuery.OrderBy(r => r.RiverName);
            return riversQuery;
        }
    }
}
