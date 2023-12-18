using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using System.Text.RegularExpressions;

namespace LAB6
{
    class Parser
    {
        async public Task Parse(string url)
        {
            IConfiguration config = Configuration.Default.WithDefaultLoader();
            IBrowsingContext context = BrowsingContext.New(config);
            IDocument doc = await context.OpenAsync(url);
            Smartphone smartphone = new Smartphone();

            IElement name = doc.QuerySelector("div.product__area-title");
            if (name != null)
                smartphone.Name = name.TextContent.Trim();

            var property = doc.QuerySelectorAll("div.product__property-value");
            if (property.Length > 0)
            {
                IElement processor = property[1];
                smartphone.Processor = processor.TextContent.Trim();

                IElement ram = property[3];
                smartphone.Ram = ram.TextContent.Trim();

                IElement storage = property[4];
                smartphone.Storage = storage.TextContent.Trim();
            }

            string title = doc.QuerySelector("title").TextContent;
            Regex regex = new Regex(@"\d{1,3}(?:\s\d{3})*\sруб");
            Match match = regex.Match(title);
            string price = match.Value;
            smartphone.Price = price;
            object locker = new object();

            using (SmartphoneContext db = new SmartphoneContext())
            {
                Guid id = Guid.NewGuid();

                Smartphone sp = new Smartphone { ID = id, Name = smartphone.Name, Storage = smartphone.Storage,
                                                Price = smartphone.Price, Processor = smartphone.Processor,
                                                Ram = smartphone.Ram };
                db.Smartphones.Add(sp);

                db.SaveChanges();
                Console.WriteLine("База данных успешно обновлена");

                Console.WriteLine(smartphone.GetInfo());
            }
        }

        async public Task<List<string>> GetLinks(string url)
        {
            Console.WriteLine("Загружаем главную страницу");
            IConfiguration config = Configuration.Default.WithDefaultLoader();
            IBrowsingContext context = BrowsingContext.New(config);
            IDocument doc = await context.OpenAsync(url);
            Console.WriteLine("Парсим данные с главной страницы");

            IEnumerable<IElement> activeElements = doc.All.Where(block => block.LocalName == "a"
                                                            && block.ParentElement.LocalName == "div"
                                                            && block.ParentElement.ClassList.Contains("product-preview__title"));

            List<string> output = new List<string>();
            foreach (IElement elem in activeElements.ToList())
                output.Add($"https://2droida.ru{elem.GetAttribute("href")}");
            Console.WriteLine("Парсинг главной страницы успешно завершен");
            return output;
        }
    }
}
