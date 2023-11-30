using System.Windows;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModel();
        }

        public void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.showDB();
        }

        public void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.saveDB();
        }
    }
}
