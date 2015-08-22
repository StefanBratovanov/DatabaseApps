namespace BookShopSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using BookShopSystem.Data;
    using System.IO;
    using BookShopSystem.Models;
    using System.Globalization;

    public sealed class Configuration : DbMigrationsConfiguration<BookShopContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "BookShopSystem.Data.BookShopContext";
        }

        Random random = new Random();



        protected override void Seed(BookShopContext context)
        {
            //using (var reader = new StreamReader("authors.txt"))
            //{

            //    var line = reader.ReadLine();
            //    line = reader.ReadLine();
            //    while (line != null)
            //    {
            //        var data = line.Split(new[] { ' ' }, 2);

            //        var firstName = data[0];
            //        var lastName = data[1];

            //        if (context.Authors.Any())
            //        {
            //            return;
            //        }

            //        context.Authors.Add(new Author()
            //        {
            //            FirstName = firstName,
            //            LastName = lastName
            //        });

            //        line = reader.ReadLine();
            //    }
            //}


            using (var reader = new StreamReader("categories.txt"))
            {

                var line = reader.ReadLine();
                line.Trim();

                while (line != null)
                {
                    var catrgory = line.Trim();

                    if (context.Categories.Any())
                    {
                        return;
                    }

                    context.Categories.Add(new Category()
                    {
                        name = catrgory
                    });

                    line = reader.ReadLine();
                }
            }


            //using (var reader = new StreamReader("books.txt"))
            //{
            //    // var authors = context.Authors;
            //    var line = reader.ReadLine();
            //    line = reader.ReadLine();
            //    while (line != null)
            //    {
            //        var data = line.Split(new[] { ' ' }, 6);
            //        var authorIndex = random.Next(0, context.Authors.Count());

            //        // var author = authors[authorIndex];
            //        var edition = (EditionType)int.Parse(data[0]);
            //        var releaseDate = DateTime.ParseExact(data[1], "d/M/yyyy", CultureInfo.InvariantCulture);
            //        var copies = int.Parse(data[2]);
            //        var price = decimal.Parse(data[3]);
            //        var ageRestriction = (Age)int.Parse(data[4]);
            //        var title = data[5];

            //        if (context.Books.Any())
            //        {
            //            return;
            //        }

            //        context.Books.Add(new Book()
            //        {
            //            Title = title,
            //            EditionType = edition,
            //            Price = price,
            //            Copies = copies,
            //            ReleaseDate = releaseDate,
            //            AgeRestriction = ageRestriction

            //        });

            //        line = reader.ReadLine();
            //    }
            //}


            context.SaveChanges();
        }
    }
}