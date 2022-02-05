using Ipme.WikiBeer.Models;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UserControls.Components
{
    /// <summary>
    /// Logique d'interaction pour BeerDetailsComponent.xaml
    /// </summary>
    public partial class BeerDetailsComponent : UserControl
    {
        public static readonly DependencyProperty BeerToModifyProperty =
            DependencyProperty.Register("BeerToModify", typeof(BeerModel), typeof(BeerDetailsComponent));

        public BeerModel BeerToModify
        {
            get { return GetValue(BeerToModifyProperty) as BeerModel; }
            set 
            { 
                if(value != null)
                {
                    SetValue(BeerToModifyProperty, value);
                }
            }
        }

        public static readonly DependencyProperty BreweriesProperty = 
            DependencyProperty.Register("Breweries", typeof(GenericListModel<BreweryModel>), typeof(BeerDetailsComponent));

        public  GenericListModel<BreweryModel> Breweries
        {
            get { return GetValue(BreweriesProperty) as GenericListModel<BreweryModel>; }
            set 
            {
                if(value != null)
                {
                    SetValue(BreweriesProperty, value);
                }
            }
        }

        public static readonly DependencyProperty StylesProperty =
            DependencyProperty.Register("Styles", typeof(GenericListModel<BeerStyleModel>), typeof(BeerDetailsComponent));

        public GenericListModel<BeerStyleModel> Styles
        {
            get { return GetValue(StylesProperty) as GenericListModel<BeerStyleModel>; }
            set
            {
                if (value != null)
                {
                    SetValue(StylesProperty, value);
                }
            }
        }

        public static readonly DependencyProperty ColorsProperty =
           DependencyProperty.Register("Colors", typeof(GenericListModel<BeerColorModel>), typeof(BeerDetailsComponent));

        public GenericListModel<BeerColorModel> Colors
        {
            get { return GetValue(ColorsProperty) as GenericListModel<BeerColorModel>; }
            set
            {
                if (value != null)
                {
                    SetValue(ColorsProperty, value);
                }
            }
        }

        public BeerDetailsComponent()
        {
            InitializeComponent();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
