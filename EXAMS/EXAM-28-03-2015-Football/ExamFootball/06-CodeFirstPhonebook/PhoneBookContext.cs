namespace _06_CodeFirstPhonebook
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PhoneBookContext : DbContext
    {
        
        public PhoneBookContext()
            : base("name=PhoneBookContext")
        {
        }

        public IDbSet<Phone> Phones { get; set; }
        public IDbSet<Email> Emails { get; set; }
        public IDbSet<Contact> Contacts { get; set; }
    }

    
}