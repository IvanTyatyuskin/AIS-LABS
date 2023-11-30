using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp1.Controller;
using System.IO;

namespace ConsoleApp1
{
    class View
    {
        public static bool IsValidString(string input)
        {
            return !string.IsNullOrEmpty(input) && input.All(c => char.IsLetter(c)) && Char.IsUpper(input[0]);
        }

        public static bool ConvertToBool(string input)
        {
            if (input == "да")
                return true;
            else
                return false;
        }

        static void Main(string[] args)
        {
            bool esc = false;
            while (!esc)
            {
                Console.Clear();
                Console.WriteLine("1) Ввести путь .csv файла \n" +
                    "2) Вывести все записи на экран \n" +
                    "3) Вывести запись по номеру \n" +
                    "4) Добавить запись в файл \n" +
                    "5) Удалить запись по номеру");
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        Controller.readDB();
                        Console.WriteLine(Controller.showDB());
                        Console.ReadKey(true);
                        break;
                    case ConsoleKey.D2:
                        Console.Clear();
                        if (Controller.getLength() == 0)
                        {
                            Console.WriteLine("Таблица пуста, невозможно вывести записи");
                            Console.ReadKey(true);
                            break;
                        }
                        Console.WriteLine(Controller.showDB());
                        Console.ReadKey(true);
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        if (Controller.getLength() == 0)
                        {
                            Console.WriteLine("Таблица пуста, невозможно вывести запись по номеру");
                            Console.ReadKey(true);
                            break;
                        }
                        Console.Write("Введите номер записи для отображения между 1 и " + Convert.ToString(Controller.getLength()) + ": ");
                        int index;
                        while (!int.TryParse(Console.ReadLine(), out index) || !(index > 0 && index <= Controller.getLength()))
                            Console.WriteLine("Введите корректный индекс между 1 и " + Convert.ToString(Controller.getLength()) + ": ");
                        Console.WriteLine(Controller.showRecord(index));
                        Console.ReadKey(true);
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        Console.Write("Введите фамилию: ");
                        string lastName = Console.ReadLine();
                        while (!IsValidString(lastName))
                        {
                            Console.WriteLine("Введите фамилию корректно - без цифр или специальных символов, начиная с большой буквы: ");
                            lastName = Console.ReadLine();
                        }
                        Console.Write("Введите имя: ");
                        string firstName = Console.ReadLine();
                        while (!IsValidString(firstName))
                        {
                            Console.WriteLine("Введите имя корректно - без цифр или специальных символов, начиная с большой буквы: ");
                            firstName = Console.ReadLine();
                        }
                        Console.Write("Введите отчество: ");
                        string fatherName = Console.ReadLine();
                        while (!IsValidString(fatherName))
                        {
                            Console.WriteLine("Введите отчество корректно - без цифр или специальных символов, начиная с большой буквы: ");
                            fatherName = Console.ReadLine();
                        }
                        Console.Write("Введите год рождения: ");
                        int birthYear;
                        while (!int.TryParse(Console.ReadLine(), out birthYear) || !(birthYear > 1900 && birthYear < 2023))
                            Console.WriteLine("Введите корректное число года рождения от 1900 до 2023: ");
                        Console.Write("Введите 'Да' если есть домашний питомец, 'Нет' если нет: ");
                        string havePet = Console.ReadLine().ToLower();
                        while (havePet != "да" && havePet != "нет")
                        {
                            Console.WriteLine("Введите корректное значение - Да или Нет: ");
                            havePet = Console.ReadLine().ToLower();
                        }
                        Console.WriteLine(Controller.addRecord(lastName, firstName, fatherName, birthYear, ConvertToBool(havePet)));
                        Console.ReadKey(true);
                        break;
                    case ConsoleKey.D5:
                        Console.Clear();
                        if (Controller.getLength() == 0)
                        {
                            Console.WriteLine("Таблица пуста, невозможно удалить запись");
                            Console.ReadKey(true);
                            break;
                        }
                        Console.Write("Введите номер записи для удаления между 1 и " + Convert.ToString(Controller.getLength()) + ": ");
                        int delIndex;
                        while (!int.TryParse(Console.ReadLine(), out delIndex) || !(delIndex > 0 && delIndex <= Controller.getLength()))
                            Console.WriteLine("Введите корректный индекс между 1 и " + Convert.ToString(Controller.getLength()) + ": ");
                        Console.ReadKey(true);
                        break;
                }
            }
        }
    }
}
