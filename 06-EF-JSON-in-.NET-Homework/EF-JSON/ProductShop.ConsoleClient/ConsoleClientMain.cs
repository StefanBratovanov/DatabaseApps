using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace ProductShop.ConsoleClient
{
    using Data;
    using System.IO;

    public class ConsoleClientMain
    {
        static void Main()
        {
            var context = new ProductShopContext();
            var count = context.Categories.Count();
            //Console.WriteLine(count);


            // Query 1 - Products In Range.Get all products in a specified price range (e.g. 500 to 1000) which have no buyer. Order them by price (from lowest to highest). 
            //Select only the product name, price and the full name of the seller. Export the result to JSON.

            var productsInRange = context.Products
                .Where(p => p.Buyer == null && p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    p.Name,
                    p.Price,
                    SellerFullName = p.Seller.FirstName + " " + p.Seller.LastName
                });

            var serializedProducts = JsonConvert.SerializeObject(productsInRange, Formatting.Indented);
            //File.WriteAllText("../../01-products-in-range.json", serializedProducts);


            //Query 2 - Successfully Sold Products. Get all users who have at least 1 sold item with a buyer. Order them by last name, then by first name. 
            //Select the person's first and last name. For each of the sold products (products with buyers), select the product's name, 
            //price and the buyer's first and last name

            var usersSoldProducts = context.Users
                .Where(u => u.ProductsSold.Any() && u.ProductsSold.Where(p => p.Buyer.Id != null).Count() > 0)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.ProductsSold.Select(p => new
                     {
                         name = p.Name,
                         price = p.Price,
                         buyerFirstName = p.Buyer.FirstName,
                         buyerLastName = p.Buyer.LastName
                     })
                }).ToList();

            var serializedusersSoldProducts = JsonConvert.SerializeObject(usersSoldProducts, Formatting.Indented);
            //File.WriteAllText("../../02-users-sold-products.json", serializedusersSoldProducts);


            //Query 3 - Categories By Products Count. Get all categories. Order them by the number of products in that
            //category (a product can be in many categories). For each category select its name, the number of products,
            //the average price of those products and the total revenue (total price sum) of those products 
            //(regardless if they have a buyer or not).

            var categoriesByProducts = context.Categories
                .OrderByDescending(c => c.Products.Count())
                .Select(c => new
                {
                    category = c.Name,
                    productsCount = c.Products.Count(),
                    averagePrice = c.Products.Average(p => p.Price),
                    totalRevenue = c.Products.Sum(p => p.Price)
                }).ToList();

            var serializedCategories = JsonConvert.SerializeObject(categoriesByProducts, Formatting.Indented);
            //File.WriteAllText("../../03-categories-by-products.json", serializedCategories);


            //Query 4 - Users and Products. Get all users who have at least 1 sold product. Order them by the number of sold products (from highest to lowest), 
            //then by last name (ascending). Select only their first and last name, age and for each product - name and price. Export the results to XML.
            //Follow the format below to better understand how to structure your data. Note: If a user has no first name or age, do not add attributes.

            var usersTraders = context.Users
                .Where(u => u.ProductsSold.Any() && u.ProductsSold.Where(p => p.Buyer.Id != null).Count() > 0)
                .OrderByDescending(u => u.ProductsSold.Count)
                .ThenBy(u => u.LastName)
                .Select(u => new
                {
                    u.FirstName,
                    u.LastName,
                    u.Age,
                    Products = u.ProductsSold.Select(p => new
                     {
                         p.Name,
                         p.Price,
                     })
                }).ToList();

            var xmlDoc = new XDocument();
            var roorElement = new XElement("users");
            roorElement.SetAttributeValue("count", usersTraders.Count);
            xmlDoc.Add(roorElement);

            foreach (var u in usersTraders)
            {
                var userTag = new XElement("user");

                if (u.FirstName != null)
                {
                    userTag.SetAttributeValue("first-name", u.FirstName);
                }
                if (u.LastName != null)
                {
                    userTag.SetAttributeValue("last-name", u.LastName);
                }
                if (u.Age != null)
                {
                    userTag.SetAttributeValue("age", u.Age);
                }

                var productsTag = new XElement("sold-products");
                productsTag.SetAttributeValue("count", u.Products.Count());

                foreach (var p in u.Products)
                {
                    var pTag = new XElement("product");
                    pTag.SetAttributeValue("name", p.Name);
                    pTag.SetAttributeValue("price", p.Price);

                    productsTag.Add(pTag);
                }

                userTag.Add(productsTag);
                roorElement.Add(userTag);
            }

            xmlDoc.Save("../../04-users-and-products.xml", SaveOptions.None);
        }
    }
}
