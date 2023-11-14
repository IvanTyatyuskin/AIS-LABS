using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp1.Controller;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Net;

namespace ConsoleApp1
{
    class View
    {
        static Controller controller = new Controller();

        string replyStr = "";
        int index;
        bool waitingForShowIndex = false;
        bool waitingForDelIndex = false;
        bool waitingForLastName = false;
        bool waitingForFirstName = false;
        bool waitingForFatherName = false;
        bool waitingForBirthYear = false;
        bool waitingForHavePet = false;
        string lastName;
        string firstName;
        string fatherName;
        int birthYear;
        bool havePet;

        public string ViewReply(string str)
        {
            if (waitingForShowIndex)
            {
                if (!int.TryParse(str, out index) || !(controller.isIndexInDB(index)))
                    replyStr = "Записи с таким ID нет в базе данных или ID ввден неверно, введите другой ID: ";
                else
                {
                    replyStr = controller.showRecord(index);
                    waitingForShowIndex = false;
                }
            }
            else if (waitingForDelIndex)
            {
                if (!int.TryParse(str, out index) || !(controller.isIndexInDB(index)))
                    replyStr = "Записи с таким ID нет в базе данных или ID ввден неверно, введите другой ID: ";
                else
                {
                    replyStr = controller.deleteRecord(index);
                    waitingForDelIndex = false;
                }
            }
            else if (waitingForLastName)
            {
                if (!Validation.IsValidString(str))
                    replyStr = "Введите фамилию корректно - без цифр или специальных символов, начиная с большой буквы: ";
                else
                {
                    lastName = str;
                    replyStr = "Введите имя: ";
                    waitingForFirstName = true;
                    waitingForLastName = false;
                }
            }
            else if (waitingForFirstName)
            {
                if (!Validation.IsValidString(str))
                    replyStr = "Введите имя корректно - без цифр или специальных символов, начиная с большой буквы: ";
                else
                {
                    firstName = str;
                    replyStr = "Введите отчество: ";
                    waitingForFatherName = true;
                    waitingForFirstName = false;
                }
            }
            else if (waitingForFatherName)
            {
                if (!Validation.IsValidString(str))
                    replyStr = "Введите отчество корректно - без цифр или специальных символов, начиная с большой буквы: ";
                else
                {
                    fatherName = str;
                    replyStr = "Введите год рождения: ";
                    waitingForBirthYear = true;
                    waitingForFatherName = false;
                }
            }
            else if (waitingForBirthYear)
            {
                if (!int.TryParse(str, out birthYear) || !(birthYear > 1900 && birthYear < 2023))
                    replyStr = "Введите корректное число года рождения от 1900 до 2023: ";
                else
                {
                    replyStr = "Введите 'Да' если есть домашний питомец, 'Нет' если нет: ";
                    waitingForHavePet = true;
                    waitingForBirthYear = false;
                }
            }
            else if (waitingForHavePet)
            {
                string strToLower = str.ToLower();
                if (strToLower != "да" && strToLower != "нет")
                    replyStr = "Введите корректное значение - Да или Нет: ";
                else
                {
                    havePet = Validation.ConvertToBool(strToLower);
                    replyStr = controller.addRecord(lastName, firstName, fatherName, birthYear, havePet);
                    waitingForHavePet = false;
                }
            }
            else
            {
			    switch (str)
			    {
				    case "":
			            replyStr = "1) Вывести все записи на экран \n" +
									"2) Вывести запись по ID \n" +
									"3) Добавить запись в файл \n" +
									"4) Удалить запись по ID";
					    break;
				    case "1":
						if (controller.getLength() == 0)
							replyStr = "Таблица пуста, невозможно вывести записи";
						else
							replyStr = controller.showDB();
					    break;
				    case "2":
						if (controller.getLength() == 0)
							replyStr = "Таблица пуста, невозможно вывести запись по ID";
						else
						{
						    waitingForShowIndex = true;
							replyStr = "Введите ID записи для отображения: ";
						}
					    break;
				    case "3":
                        waitingForLastName = true;
                        replyStr = "Введите фамилию: ";
					    break;
				    case "4":
                        if (controller.getLength() == 0)
                            replyStr = "Таблица пуста, невозможно удалить запись по ID";
                        else
                        {
                            waitingForDelIndex = true;
						    replyStr = "Введите ID записи для удаления: ";
                        }  
					    break;
			    }
            }
            return replyStr;
        }
    }
}