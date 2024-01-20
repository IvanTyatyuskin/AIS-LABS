using OxyPlot;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows;

namespace CourseProject
{
    public partial class GraphicsReports : Window, INotifyPropertyChanged
    {
        private PlotModel _graphicsPlot;

        public PlotModel GraphicsPlot
        {
            get { return _graphicsPlot; }
            set
            {
                if (_graphicsPlot != value)
                {
                    _graphicsPlot = value;
                    OnPropertyChanged(nameof(GraphicsPlot));
                }
            }
        }

        public GraphicsReports()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void drawGraphic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton selectedRadioButton = optionsGroup.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);

                if (selectedRadioButton != null)
                {
                    string selectedOption = selectedRadioButton.Content.ToString();
                    PlotModel model = new PlotModel();

                    switch (selectedOption)
                    {
                        case "По странам":
                            model = Graphics.moviesByCountries();
                            break;
                        case "По рейтингу и году":
                            model = Graphics.moviesByRatingAndYears();
                            break;
                        case "По жанрам":
                            model = Graphics.moviesByGenres();
                            break;
                    }

                    GraphicsPlot = model;
                    GraphicsPlot.InvalidatePlot(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При попытке отобразить график возникла ошибка: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void reportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton selectedRadioButton = optionsGroup.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);
                var report = new Report("Report.docx");

                if (selectedRadioButton != null)
                {
                    string selectedOption = selectedRadioButton.Content.ToString();

                    switch (selectedOption)
                    {
                        case "По странам":
                            report.сreateReport(1);
                            break;
                        case "По рейтингу и году":
                            report.сreateReport(2);
                            break;
                        case "По жанрам":
                            report.сreateReport(3);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При попытке сохранить отчет возникла ошибка: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void saveGraphic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton selectedRadioButton = optionsGroup.Children.OfType<RadioButton>().FirstOrDefault(r => r.IsChecked == true);

                if (selectedRadioButton != null)
                {
                    string selectedOption = selectedRadioButton.Content.ToString();

                    switch (selectedOption)
                    {
                        case "По странам":
                            Graphics.saveGraphic(1);
                            break;
                        case "По рейтингу и году":
                            Graphics.saveGraphic(2);
                            break;
                        case "По жанрам":
                            Graphics.saveGraphic(3);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При попытке сохранить график возникла ошибка: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
