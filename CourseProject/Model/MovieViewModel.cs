using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Media;
using System.Windows;

namespace CourseProject.Model
{
    public class MovieViewModel
    {
        public int Number { get; set; }
        public string TitleRU { get; set; }
        public string TitleEN { get; set; }
        public int Year { get; set; }
        public int Duration { get; set; }
        public string CountryName { get; set; }
        public string GenreName { get; set; }
        public string DirectorName { get; set; }
        public string Cast { get; set; }
        public float Rating { get; set; }
        public int Votes { get; set; }

        public MovieViewModel(int number, string titleRU, string titleEN, int year, int duration, string country, string genre, string director, string cast, float rating, int votes)
        {
            Number = number;
            TitleRU = titleRU;
            TitleEN = titleEN;
            Year = year;
            Duration = duration;
            CountryName = country;
            GenreName = genre;
            DirectorName = director;
            Cast = cast;
            Rating = rating;
            Votes = votes;
        }
    }
}
