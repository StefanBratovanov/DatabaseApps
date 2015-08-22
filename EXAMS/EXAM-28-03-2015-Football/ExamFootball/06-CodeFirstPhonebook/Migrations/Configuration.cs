namespace _06_CodeFirstPhonebook.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<_06_CodeFirstPhonebook.PhoneBookContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
            ContextKey = "_06_CodeFirstPhonebook.PhoneBookContext";
        }

        protected override void Seed(_06_CodeFirstPhonebook.PhoneBookContext context)
        {
            if (!context.Contacts.Any())
            {
                var peter = new Contact()
                {
                    Name = "Peter Ivanov",
                    Position = "CTO",
                    Company = "Smart Ideas",
                    SiteURL = "http://blog.peter.com",
                    Phones = new HashSet<Phone>()
                    {
                        new Phone() { PhoneNumber = "+359 2 22 22 22" },
                        new Phone() { PhoneNumber = "+359 88 77 88 99" }
                    },
                    Emails = new HashSet<Email>()
                    {
                        new Email() { EmailAddress = "peter@gmail.com" },
                        new Email() { EmailAddress = "peter_ivanov@yahoo.com" }
                    },
                    Notes = "Friend from school"
                };
                context.Contacts.Add(peter);

                var maria = new Contact()
                {
                    Name = "Maria",
                    Phones = new HashSet<Phone>()
                    {
                        new Phone() { PhoneNumber = "+359 22 33 44 55" }
                    }
                };
                context.Contacts.Add(maria);

                var angie = new Contact()
                {
                    Name = "Angie Stanton",
                    Emails = new HashSet<Email>()
                    {
                        new Email() { EmailAddress = "info@angiestanton.com" }
                    },
                    SiteURL = "http://angiestanton.com"
                };
                context.Contacts.Add(angie);

                context.SaveChanges();
            }
        }
    }
}
