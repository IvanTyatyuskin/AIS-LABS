using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp1.Human;
using System.IO;
using NLog;


namespace ConsoleApp1
{
    class Controller
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        public string showDB()
        {
            string output = "";
            List<Human> humans;
            using (HumanContext db = new HumanContext())
            {
                humans = db.Humans.ToList();
            }
            foreach (Human elem in humans)
            {
                output += elem.lastName + "," + elem.firstName + "," + elem.fatherName + "," + Convert.ToString(elem.birthYear) + "," + Convert.ToString(elem.havePet) + ";";
            }
            return output;
        }

        public void saveDB(string input)
        {
            List<Human> humans = parseInput(input);
            try
            {
                using (HumanContext db = new HumanContext())
                {
                    db.Database.ExecuteSqlCommand("Truncate Humen");
                    foreach (Human elem in humans)
                    {
                        db.Humans.Add(elem);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Произошла ошибка при добавлении записи в базу данных: " + e.Message);
            }
        }

        public List<Human> parseInput(string input)
        {
            List<Human> humans = new List<Human>();
            string[] dataSets = input.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string elem in dataSets)
            {
                string[] attributes = elem.Split(new char[] { ',' });
                Human human = new Human(attributes[0], attributes[1], attributes[2], Convert.ToInt32(attributes[3]), Convert.ToBoolean(attributes[4]));
                humans.Add(human);
            }
            return humans;
        }
    }
}
