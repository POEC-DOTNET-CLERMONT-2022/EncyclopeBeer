using Ipme.WikiBeer.Models;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UserControls.Components
{
    /// <summary>
    /// Logique d'interaction pour BreweryListComponent.xaml
    /// </summary>
    public partial class BreweryListComponent : UserControl
    {
        public static readonly DependencyProperty DataListProperty =
            DependencyProperty.Register("DataList", typeof(GenericListModel<BreweryModel>), typeof(BreweryListComponent));

        // La DepencyProperty renvoie forcément une property qui est Observable
        // par construction donc ce n'est pas la peine de lui affecter IObservable
        public GenericListModel<BreweryModel> DataList
        {
            get { return GetValue(DataListProperty) as GenericListModel<BreweryModel>; }
            set
            {
                if (value != null)
                {
                    SetValue(DataListProperty, value);
                }
            }
        }

        public BreweryListComponent()
        {
            InitializeComponent();
        }
    }
}
