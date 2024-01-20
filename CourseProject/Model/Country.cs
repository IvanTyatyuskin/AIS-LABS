using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int countryId { get; set; }

        [Column("Страна производства")]
        public string name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

        public Country()
        {
            Movies = new HashSet<Movie>();
        }

        public Country(string countryName)
        {
            name = countryName;
        }

        public int getID()
        {
            return countryId;
        }
    }
}
