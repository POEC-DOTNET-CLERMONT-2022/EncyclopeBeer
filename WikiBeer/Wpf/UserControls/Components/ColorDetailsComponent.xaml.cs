using Ipme.WikiBeer.Models;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UserControls.Components
{
    /// <summary>
    /// Logique d'interaction pour ColorDetailsComponent.xaml
    /// </summary>
    public partial class ColorDetailsComponent : UserControl
    {
        public static readonly DependencyProperty ColorDetailsProperty =
                    DependencyProperty.Register("ColorDetails", typeof(BeerColorModel), typeof(ColorDetailsComponent));

        public BreweryModel ColorDetails
        {
            get { return GetValue(ColorDetailsProperty) as BreweryModel; }
            set
            {
                if (value != null)
                {
                    SetValue(ColorDetailsProperty, value);
                }
            }
        }

        public ColorDetailsComponent()
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
