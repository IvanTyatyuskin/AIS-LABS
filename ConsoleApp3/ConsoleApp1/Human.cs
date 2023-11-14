using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Human
    {
        private int Id;
        private string LastName;
        private string FirstName;
        private string FatherName;
        private int BirthYear;
        private bool HavePet;

        public int id
        {
            get => Id;
            set => Id = value;
        }
        public string lastName
        {
            get => LastName;
            set => LastName = value;
        }
        public string firstName
        {
            get => FirstName;
            set => FirstName = value;
        }
        public string fatherName
        {
            get => FatherName;
            set => FatherName = value;
        }
        public int birthYear
        {
            get => BirthYear;
            set => BirthYear = value;
        }
        public bool havePet
        {
            get => HavePet;
            set => HavePet = value;
        }

        public Human()
        {

        }

        public Human(string LastName, string FirstName, string FatherName, int BirthYear, bool HavePet)
        {
            lastName = LastName;
            firstName = FirstName;
            fatherName = FatherName;
            birthYear = BirthYear;
            havePet = HavePet;
        }
    }
}
