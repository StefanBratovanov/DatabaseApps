using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_CodeFirstPhonebook
{
    public class Contact
    {
        private ICollection<Email> emails;
        private ICollection<Phone> phones;

        public Contact()
        {
            this.emails = new HashSet<Email>();
            this.phones = new HashSet<Phone>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Position { get; set; }

        public string Company { get; set; }

        [DataType(DataType.Url, ErrorMessage = "Not a valid URL")]
        public string SiteURL { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<Email> Emails
        {
            get { return this.emails; }
            set { this.emails = value; }
        }

        public virtual ICollection<Phone> Phones
        {
            get { return this.phones; }
            set { this.phones = value; }
        }


    }
}
