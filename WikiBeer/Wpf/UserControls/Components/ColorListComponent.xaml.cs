using Ipme.WikiBeer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
