using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int genreId { get; set; }

        [Column("Жанр")]
        public string name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

        public Genre()
        {
            Movies = new HashSet<Movie>();
        }

        public Genre(string genreName)
        {
            name = genreName;
        }
        public int getID()
        {
            return genreId;
        }
    }
}
