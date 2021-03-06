namespace BookShopSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class BookShopContext : DbContext
    {
        public BookShopContext()
            : base("name=BookShopSystem.Data.BookShopContext")
        {
        }

        public IDbSet<Book> Books { get; set; }
        public IDbSet<Author> Authors { get; set; }
        public IDbSet<Category> Categories { get; set; }

    }
}