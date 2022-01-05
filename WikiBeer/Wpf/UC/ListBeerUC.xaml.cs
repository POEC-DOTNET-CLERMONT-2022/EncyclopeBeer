using Ipme.WikiBeer.Persistance;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UC
{
    /// <summary>
    /// Logique d'interaction pour ListBeerUC.xaml
    /// </summary>
    public partial class ListBeerUC : UserControl
    {
        public IBeerManager BeerManager { get; }

        public ListBeerUC()
        {
            InitializeComponent();
            BeerManager = new BeerManager();
            BeerList.ItemsSource = BeerManager.GetAllBeer();
        }
    }
}
