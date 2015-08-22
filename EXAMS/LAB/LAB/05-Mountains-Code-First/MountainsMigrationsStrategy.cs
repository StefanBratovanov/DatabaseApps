using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _05_Mountains_Code_First
{
    public class MountainsMigrationsStrategy : DropCreateDatabaseIfModelChanges<MountainsContext>
    {
        protected override void Seed(MountainsContext context)
        {
            var bulgaria = new Country { CountryCode = "BG", CountryName = "Bulgaria" };
            context.Countries.Add(bulgaria);
            var germany = new Country { CountryCode = "DE", CountryName = "Germany" };
            context.Countries.Add(germany);

            var rila = new Mountain { Name = "Rila", Countries = { bulgaria } };
            context.Mountains.Add(rila);
            var pirin = new Mountain { Name = "Pirin", Countries = { bulgaria } };
            context.Mountains.Add(pirin);
            var rhodopes = new Mountain { Name = "Rhodopes", Countries = { bulgaria } };
            context.Mountains.Add(rhodopes);

            var musala = new Peak { Name = "Musala", Mountain = rila, Elevation = 2925 };
            context.Peaks.Add(musala);
            var malyovitsa = new Peak { Name = "Malyovitsa", Mountain = rila, Elevation = 2729 };
            context.Peaks.Add(malyovitsa);
            var vihren = new Peak { Name = "Vihren", Mountain = pirin, Elevation = 2914 };
            context.Peaks.Add(vihren);
        }
    }
}
