using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB6
{
    class Program
    {
        static async Task Main(string[] args)
        {
            /*
            Parser parser = new Parser();

            for (int i = 1; i < 24; i++)
            {
                string mainPage = $"https://2droida.ru/collection/smartfony?page={i}";
                List<string> urls = await parser.GetLinks(mainPage);
                foreach (string url in urls)
                    await parser.Parse(url);
            }
            */

            using (SmartphoneContext db = new SmartphoneContext())
            {
                var smartphones = db.Smartphones;
                foreach (Smartphone elem in smartphones)
                {
                    Console.WriteLine($"Модель: {elem.Name} \nПроцессор: {elem.Processor} \nЦена: {elem.Price} \n" +
                    $"Оперативная память: {elem.Ram} \nОбъем памяти: {elem.Storage} \n");
                }
            }
            Console.ReadLine();
        }
    }
}
