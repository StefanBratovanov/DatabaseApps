using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_CodeFirstPhonebook
{
    public class Email
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Not a vaild Email address")]
        public string EmailAddress { get; set; }

        public int ContactId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
