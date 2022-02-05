using Ipme.WikiBeer.Models;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UserControls.Components
{
    /// <summary>
    /// Logique d'interaction pour BreweryDetailsComponent.xaml
    /// </summary>
    public partial class BreweryDetailsComponent : UserControl
    {
        public static readonly DependencyProperty BreweryToModifyProperty =
                    DependencyProperty.Register("BreweryToModify", typeof(BreweryModel), typeof(BeerDetailsComponent));

        public BreweryModel BreweryToModify
        {
            get { return GetValue(BreweryToModifyProperty) as BreweryModel; }
            set
            {
                if (value != null)
                {
                    SetValue(BreweryToModifyProperty, value);
                }
            }
        }

        public static readonly DependencyProperty CountriesProperty =
            DependencyProperty.Register("Countries", typeof(GenericListModel<CountryModel>), typeof(BeerDetailsComponent));

        public GenericListModel<CountryModel> Countries
        {
            get { return GetValue(CountriesProperty) as GenericListModel<CountryModel>; }
            set
            {
                if (value != null)
                {
                    SetValue(CountriesProperty, value);
                }
            }
        }

        public BreweryDetailsComponent()
        {
            InitializeComponent();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
