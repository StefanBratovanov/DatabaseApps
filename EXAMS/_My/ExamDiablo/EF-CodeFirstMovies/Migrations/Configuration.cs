namespace EF_CodeFirstMovies.Migrations
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;


    internal sealed class Configuration : DbMigrationsConfiguration<EF_CodeFirstMovies.MoviesContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "EF_CodeFirstMovies.MoviesContext";
        }

        protected override void Seed(EF_CodeFirstMovies.MoviesContext context)
        {
            SeedCountries(context);
            SeedMovies(context);
          //  SeedUsers(context);
        }

        private void SeedMovies(MoviesContext context)
        {
            using (StreamReader r = new StreamReader("../../movies.json"))
            {
                var jsonmovies = r.ReadToEnd();
                List<Movie> moviesObj = JsonConvert.DeserializeObject<List<Movie>>(jsonmovies);

                if (context.Movies.Any())
                {
                    return;
                }

                foreach (var item in moviesObj)
                {
                    var movie = new Movie()
                    {
                        Isbn = item.Isbn,
                        Title = item.Title,
                        AgeRestriction = item.AgeRestriction
                    };
                    context.Movies.AddOrUpdate(movie);
                }
            }
            context.SaveChanges();
        }

        private void SeedUsers(MoviesContext context)
        {

            var jsonUsers = File.ReadAllText("../../users.json");

            JArray users = JArray.Parse(jsonUsers);

            foreach (JToken user in users)
            {
                User dbUser = new User();

                if (user["username"] != null)
                {
                    dbUser.UserName = user["username"].ToString();
                }


                if (user["email"] != null)
                {
                    dbUser.Email = user["email"].ToString();
                }


                if (user["age"] != null)
                {
                    dbUser.Age = int.Parse(user["age"].ToString());
                }


                if (user["country"] != null)
                {
                    var countryId = context.Users
                                    .Where(x => x.Country.Name == user["country"].ToString())
                                    .Select(x => x.Id)
                                    .FirstOrDefault();


                    dbUser.CountryId = countryId;
                }

                if (context.Users.Any())
                {
                    return;
                }
                context.Users.AddOrUpdate(dbUser);
            }
            context.SaveChanges();
        }


        private void SeedCountries(MoviesContext context)
        {
            using (StreamReader r = new StreamReader("../../countries.json"))
            {
                var jsonCountry = r.ReadToEnd();
                List<Country> countriesObj = JsonConvert.DeserializeObject<List<Country>>(jsonCountry);

                if (context.Country.Any())
                {
                    return;
                }

                foreach (var item in countriesObj)
                {
                    var country = new Country();
                    country.Name = item.Name;

                    context.Country.AddOrUpdate(country);
                }
            }
            context.SaveChanges();
        }

        /*  private void SeedUsers(MoviesContext context)
          {
              using (StreamReader r = new StreamReader("../../users.json"))
              {

                  var jsonUsers = r.ReadToEnd();
                  List<User> userssObj = JsonConvert.DeserializeObject<List<User>>(jsonUsers);

                  if (context.Users.Any())
                  {
                      return;
                  }

                  foreach (var item in userssObj)
                  {
                      User template = new User
                      {
                          UserName = item.UserName,
                          Email = item.Email,
                          Age = item.Age,
                          CountryId = context.Country.FirstOrDefault(c => c.Name == item.Country.Name).Id
                      };

                      context.Users.AddOrUpdate(template);
                  }
              }
              context.SaveChanges();
          }

          */
    }
}



