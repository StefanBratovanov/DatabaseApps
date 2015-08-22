using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics;

namespace Performance
{
    class Tests
    {
        static void Main()
        {
            var context = new AdsEntities();

            /* Problem 1. Using Entity Framework write a SQL query to select all ads from the database and later print their title,
               status, category, town and user. Do not use Include(…) for the relationships of the Ads. Check how many SQL 
               commands are executed with the SQL ExpressProfiler (or a similar tool).
             
               Add Include(…) to select statuses, categories, towns and users along with all ads. Compare the number of executed SQL
               statements and the performance before and after adding Include(…).*/


            string query = "CHECKPOINT; DBCC DROPCLEANBUFFERS;";
            var nativeQueryResult = context.Database.SqlQuery<string>(query);

            /*
            // Count: 1 
           
            var ads = context.Ads
                .Select(a => new
                {
                    a.Title,
                    Status = a.AdStatus.Status,
                    Cat = a.Category.Name,
                    Town = a.Town.Name,
                    User = a.AspNetUser.Name
                });

            foreach (var a in ads)
            {
                Console.WriteLine("{0}; {1}; Category: {2}; Town: {3}; User: {4}", a.Title, a.Status, a.Cat, a.Town, a.User);
            }
            */


            /*
            // Count: 28
            var adss = context.Ads;

            foreach (var a in adss)
            {
                Console.WriteLine("{0}; {1}; Category: {2}; Town: {3}; User: {4}",
                    a.Title,
                    a.AdStatus.Status,
                    (a.CategoryId != null) ? a.Category.Name : "N/A",
                    (a.TownId != null) ? a.Town.Name : "N/A",
                    a.AspNetUser.Name);
            }
            */


            // Count: 1
            /*
            var adsInclude = context.Ads
                .Include(a => a.AdStatus)
                .Include(a => a.Category)
                .Include(a => a.Town)
                .Include(a => a.AspNetUser);

            foreach (var a in adsInclude)
            {
                Console.WriteLine("{0}; {1}; Category: {2}; Town: {3}; User: {4}",
                    a.Title,
                    a.AdStatus.Status,
                    (a.CategoryId != null) ? a.Category.Name : "N/A",
                    (a.TownId != null) ? a.Town.Name : "N/A",
                    a.AspNetUser.Name);
            }
            */

            /* Problem 2 .Using Entity Framework select all ads from the database, then invoke ToList(), then filter the categories 
            whose status is Published; then select the ad title, category and town, then invoke ToList() again and finally order the
            ads by publish date. Rewrite the same query in a more optimized way and compare the performance. Compare the execution 
            time of the two programs. Hint: use the System.Diagnostics.Stopwatch class. Run each program 10 times and write the average
            performance time in a table */

            var sw = new Stopwatch();

            /*
            sw.Start();
            var adsPub = context.Ads
                      .ToList()
                      .Where(a => a.AdStatus.Status == "Published")
                      .Select(a => new
                      {
                          a.Title,
                          Categoty = (a.CategoryId != null) ? a.Category.Name : "N/A",
                          Town = (a.TownId != null) ? a.Town.Name : "N/A",
                          a.Date
                      })
                      .ToList()
                      .OrderBy(a => a.Date);

            Console.WriteLine("Non-optimized: {0}", sw.Elapsed);
            //Console.WriteLine(adsPub.Count());

            sw.Restart();
            var adsOpt = context.Ads
                      .Where(a => a.AdStatus.Status == "Published")
                      .OrderBy(a => a.Date)
                      .Select(a => new
                      {
                          a.Title,
                          Categoty = a.Category.Name,
                          Town = a.Town.Name
                      });

            Console.WriteLine("Optimized: {0}", sw.Elapsed);
            //Console.WriteLine(adsOpt.Count());
            */

            /* Problem 3. Write a program to compare the execution speed between these two scenarios: 
               Select everything from the Ads table and print only the ad title. Select the ad title from Ads table and print it. 
               Run the two queries 10 times and write down the average time */

            sw.Start();
            var adsEverything = context.Ads;

            foreach (var a in adsEverything)
            {
                Console.WriteLine(a.Title);
            }
            Console.WriteLine("Select All: {0}", sw.Elapsed);


            sw.Restart();
            var adsSelect = context.Ads.Select(a => a.Title);

            foreach (var a in adsSelect)
            {
                Console.WriteLine(a);
            }
            Console.WriteLine("Select specific: {0}", sw.Elapsed);


        }
    }
}
