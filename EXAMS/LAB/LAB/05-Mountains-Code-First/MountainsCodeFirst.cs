using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Mountains_Code_First
{
    class MountainsCodeFirst
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new MountainsMigrationsStrategy());

            var context = new MountainsContext();
            var cCount = context.Countries.Count();

            //Country c = new Country() { CountryCode = "AB", CountryName = "Absurdistan" };
            //Mountain m = new Mountain() { Name = "Absurdistan Hills" };
            //m.Peaks.Add(new Peak() { Name = "Great Peak", Mountain = m });
            //m.Peaks.Add(new Peak() { Name = "Small Peak", Mountain = m });

            //c.Mountains.Add(m);
            //context.Countries.Add(c);
            //context.SaveChanges();

            var mountains = context.Mountains
                .Select(m => new
                {
                    m.Name,
                    Countries = m.Countries.Select(c => c.CountryName),
                    Peaks = m.Peaks.Select(p => p.Name)
                }).ToList();

            foreach (var mountain in mountains)
            {
               // Console.WriteLine("Mname: {0}; Countries: {1}; Peaks :{2}", mountain.Name, string.Join(", ",mountain.Countries), string.Join(", ", mountain.Peaks));
            }

            var countries = context.Countries
                .Select(c => new
                {
                    c.CountryName,
                    Mountains = c.Mountains,
                    Peaks = c.Mountains.Select(m => m.Peaks)
                });

            foreach (var country in countries)
            {
                Console.WriteLine("Country: {0}", country.CountryName);
                foreach (var m in country.Mountains)
                {
                    Console.WriteLine("   Mountain: {0}", m.Name);
                    foreach (var peak in m.Peaks)
                    {
                        Console.WriteLine("\t{0} ({1})",peak.Name, peak.Elevation);
                    }
                }
            }

        }
    }
}
