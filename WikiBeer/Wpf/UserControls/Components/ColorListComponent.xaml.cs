using Ipme.WikiBeer.Models;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UserControls.Components
{
    /// <summary>
    /// Logique d'interaction pour ColorListComponent.xaml
    /// </summary>
    public partial class ColorListComponent : UserControl
    {
        public static readonly DependencyProperty DataListProperty =
    DependencyProperty.Register("DataList", typeof(GenericListModel<BeerColorModel>), typeof(ColorListComponent));

        // La DepencyProperty renvoie forcément une property qui est Observable
        // par construction donc ce n'est pas la peine de lui affecter IObservable
        public GenericListModel<BeerColorModel> DataList
        {
            get { return GetValue(DataListProperty) as GenericListModel<BeerColorModel>; }
            set
            {
                if (value != null)
                {
                    SetValue(DataListProperty, value);
                }
            }
        }
        public ColorListComponent()
        {
            InitializeComponent();
        }
    }
}
