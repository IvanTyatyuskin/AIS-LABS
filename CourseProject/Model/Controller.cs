using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Model
{
    public class Controller
    {
        public static void writeDataToDB(IEnumerable<MovieViewModel> moviesVm)
        {
            using (var context = new MovieContext())
            {
                context.Database.ExecuteSqlCommand("Truncate table Movies");
                context.Database.ExecuteSqlCommand("Delete from Genres");
                context.Database.ExecuteSqlCommand("Delete from Countries");
                context.Database.ExecuteSqlCommand("Delete from Directors");


                foreach (var mv in moviesVm)
                {
                    // Проверяем, есть ли страна в БД
                    var countryEntity = context.Countries.FirstOrDefault(c => c.name == mv.CountryName);
                    if (countryEntity == null)
                    {
                        countryEntity = context.Countries.Add(new Country(mv.CountryName));
                        context.SaveChanges(); // Сохраняем, чтобы получить ID для вновь созданной страны
                    }

                    // Проверяем, есть ли жанр в БД
                    var genreEntity = context.Genres.FirstOrDefault(g => g.name == mv.GenreName);
                    if (genreEntity == null)
                    {
                        genreEntity = context.Genres.Add(new Genre(mv.GenreName));
                        context.SaveChanges(); // Сохраняем, чтобы получить ID для вновь созданного жанра
                    }

                    // Проверяем, есть ли режиссер в БД
                    var directorEntity = context.Directors.FirstOrDefault(d => d.name == mv.DirectorName);
                    if (directorEntity == null)
                    {
                        directorEntity = context.Directors.Add(new Director(mv.DirectorName));
                        context.SaveChanges(); // Сохраняем, чтобы получить ID для вновь созданного режиссера
                    }

                    // Создаем фильм и добавляем его в БД
                    var movieEntity = new Movie(
                        mv.Number, mv.TitleRU, mv.TitleEN, mv.Year, mv.Duration,
                        countryEntity.countryId, genreEntity.genreId, directorEntity.directorId,
                        mv.Cast, mv.Rating, mv.Votes
                    );

                    context.Movies.Add(movieEntity);
                }

                // Сохраняем все созданные фильмы
                context.SaveChanges();
            }
        }

        public static List<MovieViewModel> getDataFromDB()
        {
            List<MovieViewModel> Movies = new List<MovieViewModel>();
            using (MovieContext db = new MovieContext())
            {
                var query = from movie in db.Movies
                            join country in db.Countries on movie.countryId equals country.countryId
                            join genre in db.Genres on movie.genreId equals genre.genreId
                            join director in db.Directors on movie.directorId equals director.directorId
                            select new
                            {
                                movie.number,
                                movie.titleRU,
                                movie.titleEN,
                                movie.year,
                                movie.duration,
                                CountryName = country.name,
                                GenreName = genre.name,
                                DirectorName = director.name,
                                movie.cast,
                                movie.rating,
                                movie.votes
                            };

                foreach (var result in query)
                {
                    Movies.Add(new MovieViewModel(result.number, result.titleRU, result.titleEN,
                                 result.year, result.duration, result.CountryName, result.GenreName,
                                 result.DirectorName, result.cast, result.rating, result.votes));
                }
            }

            return Movies;
        }
    }
}
