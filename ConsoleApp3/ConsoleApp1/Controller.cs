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
        public string showDB()
        {
            string line = "";
            using (HumanContext db = new HumanContext())
            {
                var humans = db.Humans;
                foreach (Human elem in humans)
                    line += Convert.ToString(elem.id) + ") " + elem.lastName + " " + elem.firstName + " " + elem.fatherName + " " + elem.birthYear + " " + elem.havePet + "\n";
            }
            return line;
        }

        public string showRecord(int index)
        {
            string line;
            using (HumanContext db = new HumanContext())
            {
                Human record = db.Humans.Find(index);
                line = record.lastName + " " + record.firstName + " " + record.fatherName + " " + record.birthYear + " " + record.havePet;
            }
            return line;
        }

        public string addRecord(string lastName, string firstName, string fatherName, int birthYear, bool havePet)
        {
            try
            {
                using (HumanContext db = new HumanContext())
                {
                    Human record = new Human(lastName, firstName, fatherName, birthYear, havePet);
                    db.Humans.Add(record);
                    db.SaveChanges();
                }
                return "Запись успешно добавлена";
            }
            catch(Exception e)
            {
                return "Произошла ошибка при добавлении записи в базу данных:" + e.Message;
            }
        }

        public string deleteRecord(int index)
        {
            try
            {
                using (HumanContext db = new HumanContext())
                {
                    Human human = db.Humans.Find(index);
                    db.Humans.Remove(human);
                    db.SaveChanges();
                }
                return "Запись успешно удалена";
            }
            catch(Exception e)
            {
                return "Произошла ошибка при удаление записи из базы данных" + e.Message;
            }
        }

        public bool isIndexInDB(int index)
        {
            using(HumanContext db = new HumanContext())
            {
                var humans = db.Humans;
                if (humans.Find(index) != null)
                    return true;
                else
                    return false;
            }
        }

        public int getLength()
        {
            using (HumanContext db = new HumanContext())
            {
                var humans = db.Humans;
                return humans.Count();
            }
        }
    }
}
