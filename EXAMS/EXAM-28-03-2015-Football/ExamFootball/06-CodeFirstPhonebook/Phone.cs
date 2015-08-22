using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_CodeFirstPhonebook
{
    public class Phone
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Not a valid Phone number")]
        public string PhoneNumber { get; set; }

        public int ContactId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
