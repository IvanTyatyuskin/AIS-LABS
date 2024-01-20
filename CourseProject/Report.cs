using CourseProject.Model;
using OxyPlot;
using OxyPlot.Wpf;
using System;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace CourseProject
{
    class Report
    {
        private FileInfo _fileInfo;
        private PlotModel _graphModel;


        public Report(string fileName)
        {
            if (File.Exists(fileName))
            {
                _fileInfo = new FileInfo(fileName);
            }
            else
            {
                throw new ArgumentException("Файл не найден");
            }
        }

        internal void сreateReport(int option)
        {
            try
            {
                using (var doc = DocX.Load(_fileInfo.FullName))
                {
                    string optionName = "";
                    List<MovieViewModel> query = new List<MovieViewModel>();

                    query = Controller.getDataFromDB();

                    var amountOfMovies = query.Count();

                    doc.ReplaceText("<amountOfMovies>", amountOfMovies.ToString());

                    float bestRating = query.Max(movie => movie.Rating);
                    doc.ReplaceText("<bestRating>", bestRating.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture));

                    float worstRating = query.Min(movie => movie.Rating);
                    doc.ReplaceText("<worstRating>", worstRating.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture));

                    var movieWithMaxVotes = query.OrderByDescending(movie => movie.Votes).FirstOrDefault();
                    doc.ReplaceText("<mostVotedMovie>", movieWithMaxVotes.TitleRU.Replace("\n", ""));

                    string str = "Режиссер не найден";
                    var directorWithMostMovies = query
                        .GroupBy(movie => movie.DirectorName)
                        .OrderByDescending(group => group.Count())
                        .FirstOrDefault();
                    str = directorWithMostMovies.Key;
                    doc.ReplaceText("<mostProductiveDirector>", str);

                    double averageDuration = query.Average(movie => movie.Duration);
                    str = Convert.ToString((int)Math.Round(averageDuration, MidpointRounding.AwayFromZero));
                    doc.ReplaceText("<averageDuration>", str);


                    string allMovies = "";

                    PlotModel model = new PlotModel();

                    switch (option)
                    {
                        case 1:
                            Dictionary<string, string> countryDic = new Dictionary<string, string>();
                            model = Graphics.moviesByCountries();
                            optionName = "По странам";
                            doc.ReplaceText("<moviesOption>", " распределенных по странам");

                            foreach (var elem in query)
                            {
                                if (countryDic.ContainsKey(elem.CountryName))
                                {
                                    countryDic[elem.CountryName] += elem.Number + ". " + elem.TitleRU;
                                    if (elem.TitleEN != "No info")
                                        countryDic[elem.CountryName] += "\nОригинальное название: " + elem.TitleEN;
                                    countryDic[elem.CountryName] += "\nГод выпуска: " + elem.Year + ", длительность: " + elem.Duration + " мин.";
                                    countryDic[elem.CountryName] += "\nЖанр: " + elem.GenreName + ", режиссер: " + elem.DirectorName;
                                    countryDic[elem.CountryName] += "\nВ ролях: " + elem.Cast;
                                    countryDic[elem.CountryName]  += "\nРейтинг КиноПоиска: " + elem.Rating.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture) + ", количество голосов: " + elem.Votes + "\n\n";
                                }
                                else
                                {
                                    string startStr = "СТРАНА ПРОИЗВОДСТВА - " + elem.CountryName + "\n\n";
                                    startStr += elem.Number + ". " + elem.TitleRU;
                                    if (elem.TitleEN != "No info")
                                        startStr += "\nОригинальное название: " + elem.TitleEN;
                                    startStr += "\nГод выпуска: " + elem.Year + ", длительность: " + elem.Duration + " мин.";
                                    startStr += "\nЖанр: " + elem.GenreName + ", режиссер: " + elem.DirectorName;
                                    startStr += "\nВ ролях: " + elem.Cast;
                                    startStr += "\nРейтинг КиноПоиска: " + elem.Rating.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture) + ", количество голосов: " + elem.Votes + "\n\n";
                                    countryDic.Add(elem.CountryName, startStr);
                                }
                            }
                            foreach (var country in countryDic)
                                allMovies += country.Value + "\n";
                            break;
                        case 2:
                            model = Graphics.moviesByRatingAndYears();
                            optionName = "По рейтингу и году";
                            doc.ReplaceText("<moviesOption>", "");


                            foreach (var elem in query)
                            {
                                allMovies += elem.Number + ". " + elem.TitleRU;
                                if (elem.TitleEN != "No info")
                                    allMovies += "\nОригинальное название: " + elem.TitleEN;
                                allMovies += "\nГод выпуска: " + elem.Year + ", длительность: " + elem.Duration + " мин.";
                                allMovies += "\nСтрана производства: " + elem.CountryName + ", жанр: " + elem.GenreName;
                                allMovies += "\nРежиссер: " + elem.DirectorName;
                                allMovies += "\nВ ролях: " + elem.Cast;
                                allMovies += "\nРейтинг КиноПоиска: " + elem.Rating.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture) + ", количество голосов: " + elem.Votes + "\n\n";
                            }
                            break;
                        case 3:
                            model = Graphics.moviesByGenres();
                            optionName = "По жанрам";
                            doc.ReplaceText("<moviesOption>", " распределенных по жанрам");

                            Dictionary<string, string> genreDic = new Dictionary<string, string>();
                            foreach (var elem in query)
                            {
                                if (genreDic.ContainsKey(elem.CountryName))
                                {
                                    genreDic[elem.CountryName] += elem.Number + ". " + elem.TitleRU;
                                    if (elem.TitleEN != "No info")
                                        genreDic[elem.CountryName] += "\nОригинальное название: " + elem.TitleEN;
                                    genreDic[elem.CountryName] += "\nГод выпуска: " + elem.Year + ", длительность: " + elem.Duration + " мин.";
                                    genreDic[elem.CountryName] += "\nСтрана производства: " + elem.CountryName + ", режиссер: " + elem.DirectorName;
                                    genreDic[elem.CountryName] += "\nВ ролях: " + elem.Cast;
                                    genreDic[elem.CountryName] += "\nРейтинг КиноПоиска: " + elem.Rating.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture) + ", количество голосов: " + elem.Votes + "\n\n";
                                }
                                else
                                {
                                    string startStr = "ЖАНР - " + elem.GenreName + "\n\n";
                                    startStr += elem.Number + ". " + elem.TitleRU;
                                    if (elem.TitleEN != "No info")
                                        startStr += "\nОригинальное название: " + elem.TitleEN;
                                    startStr += "\nГод выпуска: " + elem.Year + ", длительность: " + elem.Duration + " мин.";
                                    startStr += "\nСтрана производства: " + elem.CountryName + ", режиссер: " + elem.DirectorName;
                                    startStr += "\nВ ролях: " + elem.Cast;
                                    startStr += "\nРейтинг КиноПоиска: " + elem.Rating.ToString("0.0", System.Globalization.CultureInfo.InvariantCulture) + ", количество голосов: " + elem.Votes + "\n\n";
                                    genreDic.Add(elem.CountryName, startStr);
                                }
                            }
                            foreach (var country in genreDic)
                                allMovies += country.Value + "\n";
                            break;
                    }

                    doc.ReplaceText("<moviesList>", allMovies);


                    _graphModel = model;


                    if (_graphModel != null)
                    {
                        var imageStream = Graphics.SaveGraphToImage(_graphModel);
                        Image image = doc.AddImage(imageStream);
                        Picture picture = image.CreatePicture();
                        doc.InsertParagraph().AppendPicture(picture);
                    }

                    var newFileName = Path.Combine(_fileInfo.DirectoryName, $"{DateTime.Now:yyyyMMdd HHmmss} {_fileInfo.Name} {optionName}");
                    doc.SaveAs(newFileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При попытке создания отчета возникли ошибки: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
