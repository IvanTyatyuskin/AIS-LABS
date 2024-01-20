using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;
using System.Threading;
using System.Globalization;
using System.Numerics;
using System.IO;
using Newtonsoft.Json;
using System.Data;

namespace CourseProject
{
    public partial class MainWindow : Window
    {
        ViewModel vm = new ViewModel();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModel();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.parseData();
        }

        private void Load_Button_Click(object sender, RoutedEventArgs e)
        {
            vm.loadDB();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            vm.saveDB();
        }

        private void moviesGrid_Loaded(object sender, RoutedEventArgs e)
        {
            moviesGrid.Columns[0].IsReadOnly = true; 
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (moviesGrid.SelectedCells.Count > 0)
            {
                int selectedIndex = moviesGrid.SelectedIndex;
                vm.deleteRow(selectedIndex);
            }
        }

        private void graphicsReportsButton_Click(object sender, RoutedEventArgs e)
        {
            GraphicsReports graphicsReportsWindow = new GraphicsReports();

            graphicsReportsWindow.Show();
        }
    }
}