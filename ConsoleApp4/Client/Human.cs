using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client
{
    public class Human : INotifyPropertyChanged
    {
        private int Index;
        private string LastName;
        private string FirstName;
        private string FatherName;
        private int BirthYear;
        private bool HavePet;

        public int index
        {
            get { return Index; }
            set
            {
                Index = value;
            }
        }
        public string lastName
        {
            get { return LastName; }
            set
            {
                LastName = value;
                OnPropertyChanged("lastName");
            }
        }
        public string firstName
        {
            get { return FirstName; }
            set
            {
                FirstName = value;
                OnPropertyChanged("firstName");
            }
        }
        public string fatherName
        {
            get { return FatherName; }
            set
            {
                FatherName = value;
                OnPropertyChanged("fatherName");
            }
        }
        public int birthYear
        {
            get { return BirthYear; }
            set
            {
                BirthYear = value;
                OnPropertyChanged("birthYear");
            }
        }
        public bool havePet
        {
            get { return HavePet; }
            set
            {
                HavePet = value;
                OnPropertyChanged("havePet");
            }
        }

        public Human(string LastName, string FirstName, string FatherName, int BirthYear, bool HavePet)
        {
            lastName = LastName;
            firstName = FirstName;
            fatherName = FatherName;
            birthYear = BirthYear;
            havePet = HavePet;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
