namespace Export_Rivers_as_JSON
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EF_Mappings;
    using System.Web.Script.Serialization;
    using System.IO;

    class ExportRiversAsJson
    {
        static void Main()
        {
            var context = new GeographyEntities();
            var riversQuery =
                context.Rivers
                .OrderByDescending(r => r.Length)
                .Select(r => new
                {
                    riverName = r.RiverName,
                    riverLength = r.Length,
                    countries = r.Countries
                        .OrderBy(c => c.CountryName)
                        .Select(c => c.CountryName)
                });
            
            //Console.WriteLine(riversQuery);

            var json = new JavaScriptSerializer().Serialize(riversQuery.ToList());
            File.WriteAllText(@"rivers.json", json);
        }
    }
}
