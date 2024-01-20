using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using System.Data.Entity;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.Wpf;
using System.IO;

namespace CourseProject
{
    class Graphics
    {
        public static PlotModel moviesByCountries()
        {
            var model = new PlotModel { Title = "Распределение фильмов по странам производства"};
            var barSeries = new BarSeries { TrackerFormatString = "{0}\n{1}: {2}" };
            Dictionary<string, int> countries = new Dictionary<string, int>();

            using (MovieContext db = new MovieContext())
            {
                var query = from movie in db.Movies
                            join country in db.Countries on movie.countryId equals country.countryId
                            select new
                            {
                                movie.number,
                                CountryName = country.name,
                            };


                foreach (var result in query)
                {
                    if (countries.ContainsKey(result.CountryName))
                    {
                        countries[result.CountryName]++;
                    }
                    else
                    {
                        countries.Add(result.CountryName, 1);
                    }
                }
            }

            foreach (var elem in countries)
            {
                barSeries.Items.Add(new BarItem { Value = elem.Value});
            }

            model.Axes.Add(new CategoryAxis { Position = AxisPosition.Left, ItemsSource = countries.Keys });

            model.Series.Add(barSeries);

            return model;
        }

        public static PlotModel moviesByRatingAndYears()
        {
            var model = new PlotModel { Title = "Распределение фильмов по рейтингу и году выпуска" };
            var scatterSeries = new ScatterSeries();

            using (MovieContext db = new MovieContext())
            {
                var query = from movie in db.Movies
                            select new
                            {
                                movie.year,
                                movie.rating
                            };


                foreach (var result in query)
                {
                    scatterSeries.Points.Add(new ScatterPoint(result.year, result.rating));
                }
            }

            model.Series.Add(scatterSeries);

            return model;
        }

        public static PlotModel moviesByGenres()
        {
            var model = new PlotModel { Title = "Распределение фильмов по жанрам" };
            var pieSeries = new PieSeries { OutsideLabelFormat = "{1}", InsideLabelFormat=""};
            Dictionary<string, int> genres = new Dictionary<string, int>();
            int amount;

            using (MovieContext db = new MovieContext())
            {
                var query = from movie in db.Movies
                            join genre in db.Genres on movie.genreId equals genre.genreId
                            select new
                            {
                                GenreName = genre.name,
                            };

                amount = query.Count();
                foreach (var result in query)
                {
                    if (genres.ContainsKey(result.GenreName))
                    {
                        genres[result.GenreName]++;
                    }
                    else
                    {
                        genres.Add(result.GenreName, 1);
                    }
                }
            }

            foreach (var elem in genres)
            {
                pieSeries.Slices.Add(new PieSlice(elem.Key, elem.Value));
            }

            model.Series.Add(pieSeries);

            return model;
        }

        public static void saveGraphic(int option)
        {
            PlotModel model;

            switch(option)
            {
                case 1:
                    model = Graphics.moviesByCountries();
                    break;
                case 2:
                    model = Graphics.moviesByRatingAndYears();
                    break;
                case 3:
                    model = Graphics.moviesByGenres();
                    break;
                default:
                    model = Graphics.moviesByCountries();
                    break;
            }

            var imageStream = SaveGraphToImage(model);

            var fileName = $"{DateTime.Now:yyyyMMdd HHmmss} myplot.png";
            using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                imageStream.CopyTo(fileStream);
            }

            // Освобождение ресурсов
            imageStream.Dispose();
        }

        public static MemoryStream SaveGraphToImage(PlotModel plotModel)
        {
            var exporter = new PngExporter { Width = 620, Height = 400 };
            var imageStream = new MemoryStream();
            exporter.Export(plotModel, imageStream);
            imageStream.Position = 0;
            return imageStream;
        }
    }
}
