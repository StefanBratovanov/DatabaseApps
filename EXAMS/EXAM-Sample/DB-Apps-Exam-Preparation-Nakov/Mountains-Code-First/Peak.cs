using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mountains_Code_First
{
    using System.ComponentModel.DataAnnotations;

    public class Peak
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int Elevation { get; set; }

        public int MountainId { get; set; }

        public virtual Mountain Mountain { get; set; }
    }
}
