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
        public static readonly DependencyProperty BeersListProperty =
            DependencyProperty.Register("BeersList", typeof(BeersListModel), typeof(ListComponent));

        public BeersListModel BeersList
        {
            get { return GetValue(BeersListProperty) as BeersListModel; }
            set
            { 
                if(value != null)
                {
                    SetValue(BeersListProperty, value);
                }
            }
        }

        public ListComponent()
        {
            InitializeComponent();
        }
    }
}
