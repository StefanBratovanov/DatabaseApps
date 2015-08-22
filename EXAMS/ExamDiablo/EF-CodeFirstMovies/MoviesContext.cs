namespace EF_CodeFirstMovies
{
    using EF_CodeFirstMovies.Migrations;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MoviesContext : DbContext
    {

        public MoviesContext()
            : base("name=MoviesContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MoviesContext, Configuration>());
        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Rating> Ratings { get; set; }
        public IDbSet<Movie> Movies { get; set; }
        public IDbSet<Country> Country { get; set; }

    }


}