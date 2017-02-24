using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SampleApp
{
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<string> SampleCollection1 { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> SampleCollection2 { get; set; } = new ObservableCollection<string>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SampleListView1.ItemsSource = SampleCollection1;
            SampleListView2.ItemsSource = SampleCollection2;
        }

        private void ClearButton1_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SampleCollection1.Clear();
        }

        private void FillButton1_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            for (var idx = 0; idx < 500; idx++)
            {
                SampleCollection1.Add($"This is row number {idx}");
            }
        }

        private void ClearButton2_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SampleCollection2.Clear();
        }

        private void FillButton2_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            for (var idx = 500; idx > 0; idx--)
            {
                SampleCollection2.Add($"This is row number {idx}");
            }
        }
    }
}
