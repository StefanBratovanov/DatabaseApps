using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirst
{
    public class UserMessage
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public int RecipientId { get; set; }

        public virtual User Recipient { get; set; }

        public int SenderId { get; set; }

        public virtual User Sender { get; set; }

    
    }
}
