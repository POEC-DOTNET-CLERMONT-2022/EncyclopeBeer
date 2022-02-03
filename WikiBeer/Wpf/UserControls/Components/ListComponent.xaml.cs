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
            DependencyProperty.Register("BeersList", typeof(GenericListModel<BeerModel>), typeof(ListComponent));

        // La DepencyProperty renvoie forcément une property qui est Observable
        // par construction donc ce n'est pas la peine de lui affecter IObservable
        public GenericListModel<BeerModel> BeersList
        {
            get { return GetValue(BeersListProperty) as GenericListModel<BeerModel>; }
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
