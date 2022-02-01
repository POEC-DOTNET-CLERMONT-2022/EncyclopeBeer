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
        public static readonly DependencyProperty BeerDetailsProperty =
            DependencyProperty.Register("BeerDetails", typeof(BeerModel), typeof(BeerDetailsComponent));

        public BeersListModel BeerDetails
        {
            get { return GetValue(BeerDetailsProperty) as BeersListModel; }
            set 
            { 
                if(value != null)
                {
                    SetValue(BeerDetailsProperty, value);
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
    }
}
