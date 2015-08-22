using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _06_CodeFirstPhonebook;
using System.IO;
using Newtonsoft.Json.Linq;


namespace _07_ImportContactsFromJSON
{
    class ImportContactsJson
    {
        static void Main()
        {
            var context = new PhoneBookContext();

            var json = File.ReadAllText("../../contacts.json");

            JArray contacts = JArray.Parse(json);

            foreach (JToken contact in contacts)
            {
                Contact dbContact = new Contact();

                if (contact["name"] == null)
                {
                    Console.WriteLine("Error: Name is required");
                    continue;
                }

                dbContact.Name = contact["name"].ToString();

                if (contact["phones"] != null)
                {
                    foreach (var phone in contact["phones"])
                    {
                        dbContact.Phones.Add(new Phone { PhoneNumber = phone.ToString() });
                    }
                }

                if (contact["emails"] != null)
                {
                    foreach (var email in contact["emails"])
                    {
                        dbContact.Emails.Add(new Email { EmailAddress = email.ToString() });
                    }
                }


                if (contact["company"] != null)
                {
                    dbContact.Company = contact["company"].ToString();
                }

                if (contact["notes"] != null)
                {
                    dbContact.Notes = contact["notes"].ToString();
                }

                if (contact["position"] != null)
                {
                    dbContact.Position = contact["position"].ToString();
                }

                if (contact["site"] != null)
                {
                    dbContact.SiteURL = contact["site"].ToString();
                }

                context.Contacts.Add(dbContact);
                context.SaveChanges();

                Console.WriteLine("Contact {0} imported", dbContact.Name);
            }

        }
    }
}
