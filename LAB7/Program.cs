using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace LAB6
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Парсинг данных со страниц сайта(ЛР 6)
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


            //Заполняем новую таблицу данными
            /*
            using (SmartphoneContext db = new SmartphoneContext())
            {
                List<ProcessorManufacturer> pms = new List<ProcessorManufacturer>();

                pms.Add(new ProcessorManufacturer { ID = Guid.NewGuid(), Name = "MediaTek" });
                pms.Add(new ProcessorManufacturer { ID = Guid.NewGuid(), Name = "Google" });
                pms.Add(new ProcessorManufacturer { ID = Guid.NewGuid(), Name = "Qualcomm" });
                pms.Add(new ProcessorManufacturer { ID = Guid.NewGuid(), Name = "Snapdragon" });
                pms.Add(new ProcessorManufacturer { ID = Guid.NewGuid(), Name = "Apple" });
                pms.Add(new ProcessorManufacturer { ID = Guid.NewGuid(), Name = "Unisoc" });
                pms.Add(new ProcessorManufacturer { ID = Guid.NewGuid(), Name = "Exynos" });
                pms.Add(new ProcessorManufacturer { ID = Guid.NewGuid(), Name = "HiSilicon" });
                pms.Add(new ProcessorManufacturer { ID = Guid.NewGuid(), Name = "JLQ" });

                foreach(ProcessorManufacturer pm in pms)
                {
                    db.ProcessorManufacturers.Add(pm);
                    db.SaveChanges();
                }
            
            }
            */

            //Заполняем внешние ключи таблицы Smartphones

            using (SmartphoneContext db = new SmartphoneContext())
            {
                List<ProcessorManufacturer> pms = db.ProcessorManufacturers.ToList();
                List<Smartphone> sps = db.Smartphones.ToList();

                foreach(Smartphone sp in sps)
                {
                    foreach(ProcessorManufacturer pm in pms)
                    {
                        if ((sp.Processor).ToLower().Contains(pm.Name.ToLower()))
                                sp.processorManufacturerID = pm.ID;
                    }
                }
                db.SaveChanges();
            }

            //В выводе из базы данных добавилась новая строка для каждого устройства - Производитель процессора
            using (SmartphoneContext db = new SmartphoneContext())
            {
                var smartphones = db.Smartphones.Include(s => s.processorManufacturer);
                foreach (Smartphone elem in smartphones)
                {
                    Console.WriteLine($"Модель: {elem.Name} \nПроцессор: {elem.Processor} \nЦена: {elem.Price} \n" +
                                        $"Оперативная память: {elem.Ram} \nОбъем памяти: {elem.Storage}");
                    if (elem.processorManufacturerID != null)
                        Console.WriteLine($"Производитель процессора: {elem.processorManufacturer.Name}");
                    else
                        Console.WriteLine("Производитель процессора: неизвестен");
                    Console.WriteLine();
                }
            }
            Console.ReadLine();
        }
    }
}
