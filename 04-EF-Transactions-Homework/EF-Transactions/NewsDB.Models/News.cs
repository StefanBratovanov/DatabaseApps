using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NewsDB.Models
{
    public class News
    {
        public int Id { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string Content { get; set; }
    }
}
