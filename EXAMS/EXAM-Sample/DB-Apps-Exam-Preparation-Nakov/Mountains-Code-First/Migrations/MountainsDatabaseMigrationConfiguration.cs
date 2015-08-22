namespace Mountains_Code_First.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class MountainsDatabaseMigrationConfiguration : DbMigrationsConfiguration<MountainsContext>
    {
        public MountainsDatabaseMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MountainsContext context)
        {
            if (! context.Countries.Any())
            {
                Country bulgaria = new Country() { Code="BG", Name = "Bulgaria" };
                context.Countries.Add(bulgaria);
                Country germany = new Country() { Code="DE", Name = "Germany" };
                context.Countries.Add(germany);

                Mountain pirin = new Mountain() { Name = "Pirin", Countries = { bulgaria } };
                context.Mountains.Add(pirin);
                Mountain rila = new Mountain() { Name = "Rila", Countries = { bulgaria } };
                context.Mountains.Add(rila);
                Mountain rhodopes = new Mountain() { Name = "Rhodopes", Countries = { bulgaria } };
                context.Mountains.Add(rhodopes);

                Peak musala = new Peak() { Name = "Musala", Mountain = rila, Elevation = 2925 };
                context.Peaks.Add(musala);
                Peak malyovitsa = new Peak() { Name = "Malyovitsa", Mountain = rila, Elevation = 2729 };
                context.Peaks.Add(malyovitsa);
                Peak vihren = new Peak() { Name = "Vihren", Mountain = pirin, Elevation = 2914 };
                context.Peaks.Add(vihren);

                context.SaveChanges();
            }
        }
    }
}
