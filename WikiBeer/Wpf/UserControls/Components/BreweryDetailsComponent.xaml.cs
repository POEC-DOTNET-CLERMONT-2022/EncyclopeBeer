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
        public static readonly DependencyProperty BreweryDetailsProperty =
                    DependencyProperty.Register("BreweryDetails", typeof(BreweryModel), typeof(BreweryDetailsComponent));

        public BreweryModel BreweryDetails
        {
            get { return GetValue(BreweryDetailsProperty) as BreweryModel; }
            set
            {
                if (value != null)
                {
                    SetValue(BreweryDetailsProperty, value);
                }
            }
        }

        public static readonly DependencyProperty CountriesProperty =
            DependencyProperty.Register("Countries", typeof(GenericListModel<CountryModel>), typeof(BreweryDetailsComponent));

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

    }
}
