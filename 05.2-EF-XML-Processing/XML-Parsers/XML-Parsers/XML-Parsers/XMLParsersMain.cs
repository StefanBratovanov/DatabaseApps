using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace XML_Parsers
{
    class XMLParsersMain
    {
        static void Main()
        {
            XmlDocument catalog = new XmlDocument();
            catalog.Load("../../catalog.xml");

           
            //Problem 2.	DOM Parser: Extract Album Names. Write a program that extracts all album names from catalog.xml.
           
            var rootNode = catalog.DocumentElement;
            foreach (XmlNode childNodes in rootNode.ChildNodes)
            {
                Console.WriteLine("Album: {0}", childNodes["name"].InnerText);
            }
            

            //Problem 3.	DOM Parser: Extract All Artists Alphabetically. Write a program that extracts all artists in alphabetical order from catalog.xml. 
            //Keep the artists in a SortedSet<string> to avoid duplicates and to keep the artist in alphabetical order.

            var artists = new SortedSet<string>();

            foreach (XmlNode artist in catalog.DocumentElement)
            {
                artists.Add(artist["artist"].InnerText);
            }

            foreach (var a in artists)
            {
                Console.WriteLine("Artist: {0}", a);
            }
           

            //Problem 4.	DOM Parser: Extract Artists and Number of Albums. Write a program that extracts all different artists, which are found in the catalog.xml. 
            //For each artist print the number of albums in the catalogue. Use the DOM parser and a Dictionary<string, int> 
            //(use the artist name as key and the number of albums as value in the dictionary).

            var artistAndAlbums = new Dictionary<string, int>();

            foreach (XmlNode album in catalog.DocumentElement)
            {
                var artist = album["artist"].InnerText;

                if (artistAndAlbums.ContainsKey(artist))
                {
                    var numberOfAlbums = artistAndAlbums[artist];
                    numberOfAlbums++;

                    artistAndAlbums[artist] = numberOfAlbums;
                }
                else
                {
                    artistAndAlbums[artist] = 1;
                }
            }

            foreach (var item in artistAndAlbums)
            {
                Console.WriteLine("Artist: {0}, Albums: {1}", item.Key, item.Value);
                Console.WriteLine();
            }
            

            //Problem 5.	XPath: Extract Artists and Number of Albums. Implement the previous using XPath
            
            var artistAndAlbums = new Dictionary<string, int>();

            string xPathQuery = "/albums/album";
            XmlNodeList albums = catalog.SelectNodes(xPathQuery);

            foreach (XmlNode album in albums)
            {
                var artist = album["artist"].InnerText;
                
                if (artistAndAlbums.ContainsKey(artist))
                {
                    var numberOfAlbums = artistAndAlbums[artist];
                    numberOfAlbums++;

                    artistAndAlbums[artist] = numberOfAlbums;
                }
                else
                {
                    artistAndAlbums[artist] = 1;
                }
            }
            
           foreach (var item in artistAndAlbums)
           {
               Console.WriteLine("Artist: {0}, Albums: {1}", item.Key, item.Value);
               Console.WriteLine();
           }
           

            //Problem 6.	DOM Parser: Delete Albums. Using the DOM parser write a program to delete from catalog.xml all albums having price > 20. 
            //Save the result in a new file cheap-albums-catalog.xml.
            
            var rootElement = catalog.DocumentElement;

            for (int i = rootElement.ChildNodes.Count - 1; i >= 0; i--)
            {
                var albumss = rootElement.ChildNodes[i];
                if (decimal.Parse(albumss["price"].InnerText) > 20m)
                {
                    rootElement.RemoveChild(albumss);
                    //albums.ParentNode.RemoveChild(albums);
                }
            }

            catalog.Save("../../cheap-albums-catalog.xml");
            

            //Problem 7.	DOM Parser and XPath: Old Albums. Write a program, which extract from the file catalog.xml the titles and prices for 
            //all albums, published 5 years ago or earlier. Use XPath query

            string xQuery = "/albums/album";
            XmlNodeList albumsA = catalog.SelectNodes(xQuery);

            Console.WriteLine("Albums published 5 years ago or earlier:");
            Console.WriteLine();

            foreach (XmlNode album in albumsA)
            {
                if (int.Parse(album["year"].InnerText) <= DateTime.Now.AddYears(-5).Year)
                {
                    Console.WriteLine("Album name: {0}, Price: {1}", album["name"].InnerText, album["price"].InnerText);
                }
            }


            
            //Problem 8.	LINQ to XML: Old Albums. Write a program, which extract from the file catalog.xml the titles and prices for 
            //all albums, published 5 years ago or earlier. Use XDocument and LINQ to XML query.

            XDocument Xcatalog = XDocument.Load("../../catalog.xml");

            var albums = Xcatalog.Elements("albums").Elements("album")
                         .Where(a => int.Parse(a.Element("year").Value) <= DateTime.Now.Year - 5)
                         .Select(a => new
                         {
                            AlbumName = a.Element("name").Value,
                            Price = a.Element("price").Value,
                            ReleaseDate = a.Element("year").Value
                         })
                         .ToList();

            foreach (var album in albums)
            {
                Console.WriteLine("Album name: {0}, Price: {1}, Release date: {2}", album.AlbumName, album.Price, album.ReleaseDate);
            }
            

        }
    }
}
