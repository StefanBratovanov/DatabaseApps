using _01_DB_First;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace _02_ExportRiversJson
{
    class ExportRivers
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
                    Countries = r.Countries.OrderBy(c => c.CountryName).Select(c => c.CountryName)
                }).ToList();

            var JSerializer = new JavaScriptSerializer();
            var jsonRivers = JSerializer.Serialize(rivers);
            File.WriteAllText("../../rivers.json", jsonRivers);

        }
    }
}
