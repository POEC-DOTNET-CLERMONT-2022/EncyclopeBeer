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
        public static readonly DependencyProperty DataListProperty =
            DependencyProperty.Register("DataList", typeof(GenericListModel<ObservableObject>), typeof(ListComponent));

        // La DepencyProperty renvoie forcément une property qui est Observable
        // par construction donc ce n'est pas la peine de lui affecter IObservable
        public GenericListModel<ObservableObject> DataList
        {
            get { return GetValue(DataListProperty) as GenericListModel<ObservableObject>; }
            set
            { 
                if(value != null)
                {
                    SetValue(DataListProperty, value);
                }
            }
        }

        public ListComponent()
        {
            InitializeComponent();
        }
    }
}
