namespace NewsDB.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<NewsDB.Data.NewsContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "NewsDB.Data.NewsContext";
        }

        protected override void Seed(NewsDB.Data.NewsContext context)
        {
            if (context.News.Any())
            {
                return;
            }

            HashSet<News> newsToUpload = new HashSet<News>();
            newsToUpload.Add(new News { Content = "Brekaing news!!!" });
            newsToUpload.Add(new News { Content = "Shocking news!!!" });
            newsToUpload.Add(new News { Content = "Sad news!!!" });
            newsToUpload.Add(new News { Content = "Good news!!!" });
            newsToUpload.Add(new News { Content = "Great news!!!" });

            foreach (var n in newsToUpload)
            {
                context.News.Add(n);
            }
        }
    }
}
