using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data.Entity;
using System.Windows.Media;
using System.Windows;
using CourseProject.Model;

namespace CourseProject
{
    public class ViewModel
    {
        public static ObservableCollection<MovieViewModel> Movies { get; set; }

        public ViewModel()
        {
            Movies = new ObservableCollection<MovieViewModel>();
        }

        public void parseData()
        {
            Parser.parseKinopoiskTop250Pages(5);
            loadDB();
        }

        public void loadDB()
        {
            Movies.Clear();

            List<MovieViewModel> movies = Controller.getDataFromDB();

            foreach (MovieViewModel movie in movies)
            {
                Movies.Add(movie);
            }
        }

        public void saveDB()
        {
            List<MovieViewModel> movies = Movies.ToList();
            Controller.writeDataToDB(movies);
        }

        public void deleteRow(int index)
        {
            Movies.RemoveAt(index);
        }
    }
}
