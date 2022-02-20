using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Models.Ingredients;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UserControls.Components
{
    /// <summary>
    /// Logique d'interaction pour BeerDetailsComponent.xaml
    /// </summary>
    public partial class BeerDetailsComponent : UserControl
    {
        public static readonly DependencyProperty BeerDetailsProperty =
            DependencyProperty.Register("BeerDetails", typeof(BeerModel), typeof(BeerDetailsComponent));

        public BeerModel BeerDetails
        {
            get { return GetValue(BeerDetailsProperty) as BeerModel; }
            set 
            { 
                if(value != null)
                {
                    SetValue(BeerDetailsProperty, value);
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

        public static readonly DependencyProperty IngredientsProperty =
           DependencyProperty.Register("Ingredients", typeof(GenericListModel<IngredientModel>), typeof(BeerDetailsComponent));

        public GenericListModel<IngredientModel> Ingredients
        {
            get { return GetValue(ColorsProperty) as GenericListModel<IngredientModel>; }
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

        private void NumberValidationTextBox(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
