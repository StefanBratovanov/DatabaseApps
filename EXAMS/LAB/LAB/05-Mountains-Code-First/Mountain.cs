using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Mountains_Code_First
{
    public class Mountain
    {
        private ICollection<Peak> peaks;
        private ICollection<Country> countries;

        public Mountain()
        {
            this.peaks = new HashSet<Peak>();
            this.countries = new HashSet<Country>();
        }


        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Peak> Peaks
        {
            get { return this.peaks; }
            set { this.peaks = value; }
        }

        public virtual ICollection<Country> Countries
        {
            get { return this.countries; }
            set { this.countries = value; }
        }
    }
}
