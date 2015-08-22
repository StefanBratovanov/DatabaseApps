namespace Mountains_Code_First
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Mountains_Code_First.Migrations;

    public class MountainsContext : DbContext
    {
        public MountainsContext()
            : base("name=MountainsContext")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<MountainsContext,
                    MountainsDatabaseMigrationConfiguration>());
        }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Mountain> Mountains { get; set; }

        public virtual DbSet<Peak> Peaks { get; set; }
    }
}