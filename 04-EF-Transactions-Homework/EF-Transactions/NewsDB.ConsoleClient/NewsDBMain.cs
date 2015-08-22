using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsDB.Data;
using NewsDB.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace NewsDB.ConsoleClient
{
    public class NewsDBMain
    {
        static void Main()
        {
            var context = new NewsContext();
            var newsCount = context.News.Count();

            
            //// Problem 2.
            // Start two consoles simultaneously and fill the input one by one.

            HandleConcurrentUpdates();

        }


        private static void HandleConcurrentUpdates()
        {
            using (var context = new NewsContext())
            {
                var firstNews = context.News.FirstOrDefault();
                Console.WriteLine("Text from DB: {0}", firstNews.Content);

                Console.WriteLine("Enter the corrected text: ");
                var newContent = Console.ReadLine();

                firstNews.Content = newContent;

                try
                {
                    context.SaveChanges();
                    Console.WriteLine("Changes successfully saved in the DB.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    Console.WriteLine("Conflict!");
                    
                    HandleConcurrentUpdates();
                }
            }
        }


    }
}
