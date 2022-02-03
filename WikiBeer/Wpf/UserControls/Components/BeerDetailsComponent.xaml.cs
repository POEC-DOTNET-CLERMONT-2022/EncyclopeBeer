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
        
        public static readonly DependencyProperty BeersProperty =
            DependencyProperty.Register("Beers", typeof(GenericListModel<BeerModel>), typeof(BeerDetailsComponent));

        public GenericListModel<BeerModel> Beers
        {
            get { return GetValue(BeersProperty) as GenericListModel<BeerModel>; }
            set 
            { 
                if(value != null)
                {
                    SetValue(BeersProperty, value);
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

        public BeerDetailsComponent()
        {
            InitializeComponent();
        }

        private void BreweryComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BreweryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StyleComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StyleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ColorComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void IngredientsComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void IngredientsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
