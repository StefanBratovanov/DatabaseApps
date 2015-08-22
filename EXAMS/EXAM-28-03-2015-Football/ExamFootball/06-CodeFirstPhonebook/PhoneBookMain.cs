using _06_CodeFirstPhonebook.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_CodeFirstPhonebook
{
    class PhoneBookMain
    {
        static void Main()
        {
            var context = new PhoneBookContext();

            var migrationStrategy = new MigrateDatabaseToLatestVersion<PhoneBookContext, Configuration>();
            Database.SetInitializer(migrationStrategy);

            var contacts = context.Contacts
                .Select(c => new
                {
                    c.Name,
                    Phones = c.Phones.Select(p => p.PhoneNumber),
                    Emails = c.Emails.Select(e => e.EmailAddress)
                }).ToList();

            foreach (var contact in contacts)
            {
                Console.WriteLine(contact.Name);
                //foreach (var phone in contact.Phones)
                //{
                //    Console.WriteLine(phone);
                //}
                //foreach (var e in contact.Emails)
                //{
                //    Console.WriteLine(e);
                //}

                Console.WriteLine("Phones: {0}", string.Join(", ", contact.Phones));
                Console.WriteLine("Emails: {0}", string.Join(", ", contact.Emails));
                Console.WriteLine();

            }


        }
    }
}
