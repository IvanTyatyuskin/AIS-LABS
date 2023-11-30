using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp1.Human;
using System.IO;


namespace ConsoleApp1
{
    class Controller
    {
        static List<Human> DB = new List<Human>();
        static string path = @"C:\Users\tyaty\Desktop\ConsoleApp1\DB.csv";

        public static void readDB()
        {
            using (StreamReader reader = new StreamReader(path, System.Text.Encoding.Default))
            {
                DB.Clear();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] substrings = line.Split(';');
                    Human record = new Human(substrings[0], substrings[1], substrings[2], Convert.ToInt32(substrings[3]), Convert.ToBoolean(substrings[4]));
                    DB.Add(record);
                }
            }
        }

        public static string showDB()
        {
            string line = "";
            for (int i = 0; i < DB.Count; i++)
            {
                Human record = DB[i];
                line += Convert.ToString(i + 1) + ") " + record.lastName + " " + record.firstName + " " + record.fatherName + " " + record.birthYear + " " + record.havePet + "\n";
            }
            return line;
        }

        public static string showRecord(int index)
        {
            Human record = DB[index - 1];
            string line = record.lastName + " " + record.firstName + " " + record.fatherName + " " + record.birthYear + " " + record.havePet;
            return line;
        }

        public static string addRecord(string lastName, string firstName, string fatherName, int birthYear, bool havePet)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, true, System.Text.Encoding.Default))
                {
                    Human record = new Human(lastName, firstName, fatherName, birthYear, havePet);
                    DB.Add(record);
                    string line = lastName + ";" + firstName + ";" + fatherName + ";" + Convert.ToString(birthYear) + ";" + Convert.ToString(havePet);
                    writer.WriteLine(line);
                }

                return "Запись успешно добавлена";
            }
            catch(Exception e)
            {
                return "Произошла ошибка при добавлении записи в базу данных:" + e.Message;
            }
        }

        public static string deleteRecord(int index)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    DB.RemoveAt(index - 1);
                    for (int i = 0; i < DB.Count; i++)
                        writer.WriteLine(DB[i].lastName + ";" + DB[i].firstName + ";" + DB[i].fatherName + ";" + Convert.ToString(DB[i].birthYear) + ";" + Convert.ToString(DB[i].havePet));
                }
                return "Запись успешно удалена";
            }
            catch(Exception e)
            {
                return "Произошла ошибка при удаление записи из базы данных" + e.Message;
            }
        }

        public static int getLength()
        {
            return DB.Count;
        }
    }
}
