using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mountains_Code_First
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Country
    {
        public Country()
        {
            this.Mountains = new HashSet<Mountain>();    
        }

        [Key]
        [MinLength(2)]
        [MaxLength(2)]
        [Column(TypeName = "char")]
        public string Code { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Mountain> Mountains { get; set; }
    }
}
