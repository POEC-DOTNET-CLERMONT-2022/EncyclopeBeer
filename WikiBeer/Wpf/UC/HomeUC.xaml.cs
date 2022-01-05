using Ipme.WikiBeer.Persistance;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UC
{
    /// <summary>
    /// Logique d'interaction pour HomeU.xaml
    /// </summary>
    public partial class HomeUC : UserControl
    {
        public IBeerManager BeerManager { get; }

        public ListBeerUC ListBeerUC { get; set; } = new ListBeerUC();

        public HomeUC()
        {
            InitializeComponent();
            if (Application.Current is App app)
            {
                BeerManager = app.BeerManager;
            }
        }

        private void Button_Click_Stats(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Manager(object sender, RoutedEventArgs e)
        {
            if (SubMenuManager.Visibility != Visibility.Visible)
            {
                SubMenuManager.Visibility = Visibility.Visible;
            }
            else
            {
                SubMenuManager.Visibility = Visibility.Collapsed;
            }

            HomeControl.Content = ListBeerUC;
        }


        private void Button_Click_User(object sender, RoutedEventArgs e)
        {

        }
    }
}
