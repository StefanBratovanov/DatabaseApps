using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Mountains_Code_First
{
    class MountainsCodeFirst
    {
        static void Main()
        {
            

            //Console.WriteLine(countriesCount);
            Database.SetInitializer(new MountainsMigrationsStrategy());

            var context = new MountainsContext();
            var countriesCount = context.Countries.Count();

            Country c1 = new Country() { CountryCode = "AB", CountryName = "Absurdistan" };
            Mountain m1 = new Mountain() { Name = "Absurdistan Hills" };
            m1.Peaks.Add(new Peak() { Name = "Great Peak", Mountain = m1 });
            m1.Peaks.Add(new Peak() { Name = "Small Peak", Mountain = m1 });

            c1.Mountains.Add(m1);
            context.Countries.Add(c1);
            context.SaveChanges();

            var mountains = context.Mountains
                .Select(mo => new
                {
                    mo.Name,
                    Countries = mo.Countries.Select(co => co.CountryName),
                    Peaks = mo.Peaks.Select(p => p.Name)
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
                        Console.WriteLine("\t{0} ({1})", peak.Name, peak.Elevation);
                    }
                }
            }

        }
    }
}
