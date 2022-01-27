using Ipme.WikiBeer.Models;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UserControls.Components
{
    /// <summary>
    /// Logique d'interaction pour ListBeers.xaml
    /// </summary>
    public partial class ListComponent : UserControl
    {
        public static readonly DependencyProperty ListProperty =
            DependencyProperty.Register("List", typeof(BeersListModel), typeof(ListComponent));
        private BeersListModel BeersList
        {
            get { return GetValue(ListProperty) as BeersListModel; }
            set
            { 
                if(value != null)
                {
                    SetValue(ListProperty, value);
                }
            }
        }

        public ListComponent()
        {

        }

        public ListComponent(BeersListModel beersList)
        {
            InitializeComponent();
            this.BeersList = new BeersListModel();
            this.BeersList = beersList;
            var beer = new BeerModel();
            beer.Name = "Punk";
            this.BeersList.Beers.Add(beer);
        }
    }
}
