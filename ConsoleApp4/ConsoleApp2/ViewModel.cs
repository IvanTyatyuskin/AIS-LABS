using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ConsoleApp2
{
    public class ViewModel : INotifyPropertyChanged
    {
        ObservableCollection<Human> Humans { get; set; }

        public ViewModel()
        {
            Humans = new ObservableCollection<Human>
            {
                new Human("Pisun", "Pisun", "Pisunovich", 1990, true)
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
