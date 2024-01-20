using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using CourseProject.Model;


namespace CourseProject
{
    class Parser
    {
        public static void parseKinopoiskTop250Pages(int amountOfPages)
        {
            try
            {
                List<MovieViewModel> moviesList = new List<MovieViewModel>();
                string workingDirectory = Environment.CurrentDirectory;
                var cookiepath = workingDirectory + @"\cookie.txt";
                string jsonCookies = File.ReadAllText(cookiepath);

                List<Dictionary<string, object>> cookies = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonCookies);

                var driverpath = workingDirectory + @"\chromedriver-win64";
                var options = new ChromeOptions();
                options.SetLoggingPreference("performance", LogLevel.All);
                options.AddArgument("--blink-settings=imagesEnabled=false");
                var driver = new ChromeDriver(driverpath, options);

                driver.Navigate().GoToUrl("https://www.kinopoisk.ru/"); // Переход на страницу, соответствующую домену куки

                foreach (var cookieData in cookies)
                {
                    string sameSite = "None";
                    if (cookieData.TryGetValue("sameSite", out var sameSiteValueObject))
                    {
                        string sameSiteValueString = sameSiteValueObject.ToString()?.ToLower();
                        switch (sameSiteValueString)
                        {
                            case "lax":
                                sameSite = "Lax";
                                break;
                            case "strict":
                                sameSite = "Strict";
                                break;
                            case "none":
                            case "no_restriction":
                                sameSite = "None";
                                break;
                            case "unspecified":
                            default:
                                sameSite = "None";
                                break;
                        }
                    }

                    Cookie cookie = new Cookie(
                        cookieData["name"].ToString(),
                        cookieData["value"].ToString(),
                        cookieData.ContainsKey("domain") ? cookieData["domain"].ToString() : null,
                        cookieData.ContainsKey("path") ? cookieData["path"].ToString() : null,
                        cookieData.ContainsKey("expirationDate") ? DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(cookieData["expirationDate"])).UtcDateTime : (DateTime?)null,
                        cookieData.ContainsKey("secure") && Convert.ToBoolean(cookieData["secure"]),
                        cookieData.ContainsKey("httpOnly") && Convert.ToBoolean(cookieData["httpOnly"]),
                        sameSite // Используем скорректированное значение SameSite
                    );

                    driver.Manage().Cookies.AddCookie(cookie);
                }


                for (int j = 1; j < amountOfPages + 1; j++)
                {
                    string url = string.Format("https://www.kinopoisk.ru/lists/movies/top250/?page={0}", j);

                    driver.Navigate().GoToUrl(url);

                    var movieElements = driver.FindElements(By.CssSelector("div.styles_root__ti07r")).ToList();

                    int movieElementsLen = movieElements.Count();

                    Parallel.ForEach(movieElements, (movieElement) =>
                    {
                        string[] lines = movieElement.Text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                        int number = int.Parse(lines[0]);
                        string titleEN;
                        string titleRU;

                        int year = 0;
                        int duration = 0;

                        string country = "";
                        string genre = "";

                        string director = "";
                        string cast = "";

                        int shift = 0;

                        if (lines[3].StartsWith(","))
                        {
                            titleRU = lines[1];
                            titleEN = lines[2];
                        }
                        else if (lines[4].StartsWith(","))
                        {
                            titleRU = lines[2];
                            titleEN = lines[3];
                            shift = -1;
                        }
                        else if (lines[3].Trim().EndsWith(".") || lines[2].Trim().Equals("Чебурашка. Выходной"))
                        {
                            titleRU = lines[2];
                            titleEN = "No info";
                            shift = 0;
                        }
                        else
                        {
                            titleRU = lines[1];
                            titleEN = "No info";
                            shift = 1;
                        }

                        // Используем регулярные выражения для извлечения года, длительности и страны
                        var yearDurationCountryRegex = new Regex(@"(\d{4}), (\d+) мин\.");
                        var match = yearDurationCountryRegex.Match(movieElement.Text);
                        if (match.Success)
                        {
                            year = int.Parse(match.Groups[1].Value);
                            duration = int.Parse(match.Groups[2].Value);
                        }
                        else
                        {
                            year = 2023;
                        }

                        // Извлекаем страну и жанр
                        var countryGenreRegex = new Regex(@"\n(.+?) • (.+?)  Режиссёр:");
                        match = countryGenreRegex.Match(movieElement.Text);
                        if (match.Success)
                        {
                            country = match.Groups[1].Value.Trim();
                            genre = match.Groups[2].Value.Trim();
                        }

                        // Извлекаем режиссера
                        var directorRegex = new Regex(@"Режиссёр: (.+?)\n");
                        match = directorRegex.Match(movieElement.Text);
                        if (match.Success)
                        {
                            director = match.Groups[1].Value.Trim();
                        }

                        // Извлекаем актерский состав
                        var castRegex = new Regex(@"В ролях: (.+?)\n");
                        match = castRegex.Match(movieElement.Text);
                        if (match.Success)
                        {
                            cast = match.Groups[1].Value.Trim();
                        }

                        // Извлекаем рейтинг и количество голосов
                        float rating = float.Parse(lines[6 - shift].Trim(), CultureInfo.InvariantCulture);
                        int votes = int.Parse(lines[7 - shift].Replace(" ", ""));

                        moviesList.Add(new MovieViewModel(number, titleRU, titleEN, year, duration, country, genre, director, cast, rating, votes));
                    });
                }
                driver.Quit();
                moviesList.Sort((movie1, movie2) => movie1.Number.CompareTo(movie2.Number));
                Controller.writeDataToDB(moviesList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"При попытке парсинга данных возникла ошибка: {ex.Message}");
            }
        }
    }
}
