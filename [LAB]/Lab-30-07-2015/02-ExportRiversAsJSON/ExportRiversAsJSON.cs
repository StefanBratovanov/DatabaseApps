using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_EntityFrameworkMappings;
using System.Web.Script.Serialization;
using System.IO;



namespace _02_ExportRiversAsJSON
{
    class ExportRiversAsJSON
    {
        static void Main()
        {
            var context = new GeographyEntities();

            var rivers = context.Rivers
                .OrderByDescending(r => r.Length)
                .Select(r => new
                {
                    r.RiverName,
                    r.Length,
                    Countries = r.Countries
                                 .OrderBy(c => c.CountryName)
                                 .Select(c => c.CountryName)
                }).ToList();

            var JSerializer = new JavaScriptSerializer();
            var jsonRivers = JSerializer.Serialize(rivers);
            File.WriteAllText("../../rivers.json", jsonRivers);
        }
    }
}
