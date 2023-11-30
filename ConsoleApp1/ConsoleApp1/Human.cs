using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Human
    {
        private string LastName { get; set; }
        private string FirstName { get; set; }
        private string FatherName { get; set; }
        private int BirthYear { get; set; }
        private bool HavePet { get; set; }
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

        public Human(string LastNmae, string FirstName, string FatherName, int BirthYear, bool HavePet)
        {
            lastName = LastNmae;
            firstName = FirstName;
            fatherName = FatherName;
            birthYear = BirthYear;
            havePet = HavePet;
        }
    }
}
