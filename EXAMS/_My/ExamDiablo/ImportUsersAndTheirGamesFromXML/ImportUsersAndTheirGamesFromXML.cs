using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkMappings;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Globalization;

namespace ImportUsersAndTheirGamesFromXML
{
    class ImportUsersAndTheirGamesFromXML
    {
        static void Main()
        {
            var context = new DiabloEntities();
            XDocument xmlDoc = XDocument.Load("../../users-and-games.xml");

            var xUsers = xmlDoc.XPathSelectElements("/users/user");

            foreach (var xUser in xUsers)
            {
                User user = CreateUserIfNotExists(context, xUser);

            
            }


        }

        private static User CreateUserIfNotExists(DiabloEntities context, XElement xUser)
        {
            User user = null;
            var xUsername = xUser.Attribute("username").Value;

            user = context.Users.FirstOrDefault(u => u.Username == xUsername);

            if (user != null)
            {
                Console.WriteLine("User {0} already exists", xUsername);
            }

            else
            {
                var isDeleted = true;
                if (int.Parse(xUser.Attribute("is-deleted").Value) == 0)
                {
                    isDeleted = false;
                }

                var ip = xUser.Attribute("ip-address").Value;
                var regDate = xUser.Attribute("registration-date").Value;
                string format = "dd/MM/yyyy";
                DateTime regDateToImport = DateTime.ParseExact(regDate, format, CultureInfo.InvariantCulture);

                var firstNameAtt = xUser.Attribute("first-name");
                var lastNameAtt = xUser.Attribute("last-name");
                var emailAtt = xUser.Attribute("email");

                string firstName = null;
                string lastName = null;
                string email = null;

                if (firstNameAtt != null)
                {
                    firstName = firstNameAtt.Value;
                }

                if (lastNameAtt != null)
                {
                    lastName = lastNameAtt.Value;
                }

                if (emailAtt != null)
                {
                    email = emailAtt.Value;
                }

                user = new User
                {
                    Username = xUsername,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    IpAddress = ip,
                    RegistrationDate = regDateToImport,
                    IsDeleted = isDeleted
                };

                context.Users.Add(user);
                context.SaveChanges();
                Console.WriteLine("Successfully added user {0}", xUsername);
            }

            return user;
        }
    }
}
