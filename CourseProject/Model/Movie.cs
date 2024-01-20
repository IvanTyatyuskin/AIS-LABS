using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseProject.Model
{
    public class Movie
    {
        public int countryId { get; set; }
        public int genreId { get; set; }
        public int directorId { get; set; }
        [Column("№")]
        [Key]
        public int number { get; set; }

        [Column("Название на русском")]
        public string titleRU { get; set; }

        [Column("Название на английском")]
        public string titleEN { get; set; }

        [Column("Год выпуска")]
        public int year { get; set; }

        [Column("Длительность (мин.)")]
        public int duration { get; set; }

        [ForeignKey("countryId")]
        public virtual Country Country { get; set; }

        [ForeignKey("genreId")]
        public virtual Genre Genre { get; set; }

        [ForeignKey("directorId")]
        public virtual Director Director { get; set; }

        [Column("Актеры")]
        public string cast { get; set; }

        [Column("Рейтинг КП")]
        public float rating { get; set; }

        [Column("Количество голосов")]
        public int votes { get; set; }

        public Movie()
        {

        }

        public Movie(int Number, string TitleRU, string TitleEN, int Year, int Duration, int CountryID, int GenreID, int DirectorID, string Cast, float Rating, int Votes)
        {
            number = Number;
            titleRU = TitleRU;
            titleEN = TitleEN;
            year = Year;
            duration = Duration;
            countryId = CountryID; ;
            genreId = GenreID; ;
            directorId = DirectorID;
            cast = Cast;
            rating = Rating;
            votes = Votes;
        }
    }
}
