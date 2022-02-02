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
        public static readonly DependencyProperty BeersListProperty =
            DependencyProperty.Register("BeersList", typeof(BeersListModel), typeof(BeerDetailsComponent));

        public BeersListModel BeersList
        {
            get { return GetValue(BeersListProperty) as BeersListModel; }
            set 
            { 
                if(value != null)
                {
                    SetValue(BeersListProperty, value);
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
