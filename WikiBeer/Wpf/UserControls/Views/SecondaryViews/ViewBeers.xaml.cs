using Ipme.WikiBeer.Wpf.UserControls.Components;
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

namespace Ipme.WikiBeer.Wpf.UserControls.Views.SecondaryViews
{
    /// <summary>
    /// Logique d'interaction pour ViewBeers.xaml
    /// </summary>
    public partial class ViewBeers : UserControl
    {
        private ListComponent List = new ListComponent();
        public ViewBeers()
        {
            InitializeComponent();
        }

        private void BeerDetailsComponent_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
