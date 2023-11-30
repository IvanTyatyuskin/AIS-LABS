using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;


namespace Client
{
    public class ViewModel : INotifyPropertyChanged
    {
        public static ObservableCollection<Human> Humans { get; set; }

        public ViewModel()
        {
            Humans = new ObservableCollection<Human>();
        }

        public static void showDB()
        {
            Humans.Clear();
            List<Human> HmList = Model.getDB();
            int i = 1;
            foreach (Human elem in HmList)
            {
                elem.index = i;
                i++;
                Humans.Add(elem);
            }
        }

        public static void saveDB()
        {
            List<Human> HmList = new List<Human>(Humans);
            Model.sendDB(HmList);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
