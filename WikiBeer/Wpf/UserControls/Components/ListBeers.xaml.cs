using Ipme.WikiBeer.Models;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UserControls.Components
{
    /// <summary>
    /// Logique d'interaction pour ListBeers.xaml
    /// </summary>
    public partial class ListBeers : UserControl
    {
        private BeersList BeersList { get; set; }

        public ListBeers(BeersList beersList)
        {
            BeersList = beersList;
            InitializeComponent();
        }
    }
}
