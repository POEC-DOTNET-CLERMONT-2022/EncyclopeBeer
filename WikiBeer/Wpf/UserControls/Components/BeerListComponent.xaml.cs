using Ipme.WikiBeer.Models;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UserControls.Components
{
    /// <summary>
    /// Logique d'interaction pour ListBeers.xaml
    /// </summary>
    public partial class  BeerListComponent : UserControl
    {
        public static readonly DependencyProperty DataListProperty =
            DependencyProperty.Register("DataList", typeof(GenericListModel<BeerModel>), typeof(BeerListComponent));

        // La DepencyProperty renvoie forcément une property qui est Observable
        // par construction donc ce n'est pas la peine de lui affecter IObservable
        public GenericListModel<BeerModel> DataList
        {
            get { return GetValue(DataListProperty) as GenericListModel<BeerModel>; }
            set
            { 
                if(value != null)
                {
                    SetValue(DataListProperty, value);
                }
            }
        }

        public BeerListComponent()
        {
            InitializeComponent();
        }
    }
}
