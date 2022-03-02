using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Wpf.UserControls.Views;
using Ipme.WikiBeer.Wpf.Utilities;
using System.Windows;

namespace Ipme.WikiBeer.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public INavigator Navigator { get; set; } = ((App)Application.Current).Navigator;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Navigator.NavigateTo(typeof(ViewLogin));
        }
    }
}
