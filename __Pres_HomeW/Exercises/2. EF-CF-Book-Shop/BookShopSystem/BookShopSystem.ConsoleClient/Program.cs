using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShopSystem.Data;
using BookShopSystem.Models;
using BookShopSystem.Data.Migrations;
using System.Data.Entity;

namespace BookShopSystem.ConsoleClient
{
    class Program
    {
        static void Main()
        {
           
            var migrationStrategy = new MigrateDatabaseToLatestVersion<BookShopContext, Configuration>();

            Database.SetInitializer(migrationStrategy);

            var context = new BookShopContext();

            var bookCount = context.Books.Count(); //- to insialize the DB
        }
    }
}
