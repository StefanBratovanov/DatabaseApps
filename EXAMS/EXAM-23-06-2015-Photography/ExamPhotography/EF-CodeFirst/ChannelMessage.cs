using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirst
{
    public class ChannelMessage
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public int ChannelId { get; set; }

        public virtual Channel Channel { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

    }
}
