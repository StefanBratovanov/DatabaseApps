namespace ProductShop.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using Models;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;

    internal sealed class Configuration : DbMigrationsConfiguration<ProductShop.Data.ProductShopContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "ProductShop.Data.ProductShopContext";
        }

        protected override void Seed(ProductShop.Data.ProductShopContext context)
        {
            XDocument usersXML = XDocument.Load("../../users.xml");

            var rootElement = usersXML.Root;
            var users = rootElement.Elements();

            SeedUsers(context, users);
            SeedCategories(context);
            SeedProducts(context);

        }

        private static void SeedProducts(ProductShop.Data.ProductShopContext context)
        {
            using (StreamReader r = new StreamReader("../../products.json"))
            {
                if (context.Products.Any())
                {
                    return;
                }

                var jsonProd = r.ReadToEnd();
                List<Product> productsObj = JsonConvert.DeserializeObject<List<Product>>(jsonProd);

                var random = new Random();
                var categoryIDs = context.Categories.Select(c => c.Id).ToList();
                var userIDs = context.Users.Select(u => u.Id).ToList();

                foreach (var productObj in productsObj)
                {
                    var sellerId = userIDs[random.Next(0, userIDs.Count)];
                    var seller = context.Users.Find(sellerId);

                    var buyerId = userIDs[random.Next(0, userIDs.Count)];
                    var buyer = context.Users.Find(buyerId);

                    var productCategories = new List<Category>();
                    var numberOfCategories = 3;

                    for (int i = 0; i < numberOfCategories; i++)
                    {
                        var categoryId = categoryIDs[random.Next(0, categoryIDs.Count)];
                        productCategories.Add(context.Categories.Find(categoryId));
                    }

                    var currentProduct = new Product()
                    {
                        Seller = seller,
                        Name = productObj.Name,
                        Price = productObj.Price,
                        Categories = productCategories
                    };

                    if (random.Next(0, 6) % 5 != 0)
                    {
                        currentProduct.Buyer = buyer;
                    }
                    context.Products.AddOrUpdate(currentProduct);
                }
                context.SaveChanges();
            }
        }

        private static void SeedCategories(ProductShop.Data.ProductShopContext context)
        {
            using (StreamReader r = new StreamReader("../../categories.json"))
            {
                var jsonCat = r.ReadToEnd();
                List<Category> categoriesObj = JsonConvert.DeserializeObject<List<Category>>(jsonCat);

                if (context.Categories.Any())
                {
                    return;
                }

                foreach (var item in categoriesObj)
                {
                    var category = new Category();
                    category.Name = item.Name;

                    context.Categories.AddOrUpdate(category);
                }
            }
            context.SaveChanges();
        }

        private static void SeedUsers(ProductShop.Data.ProductShopContext context, IEnumerable<XElement> users)
        {
            if (context.Users.Any())
            {
                return;
            }

            foreach (var u in users)
            {
                var currentUser = new User();

                if (u.Attribute("first-name") == null)
                {
                    currentUser.FirstName = null;
                }
                else currentUser.FirstName = u.Attribute("first-name").Value;

                if (u.Attribute("last-name") != null)
                {
                    currentUser.LastName = u.Attribute("last-name").Value;
                }

                if (u.Attribute("age") == null)
                {
                    continue;
                }
                currentUser.Age = int.Parse(u.Attribute("age").Value);

                context.Users.AddOrUpdate(currentUser);
            }
            context.SaveChanges();
        }
    }
}

