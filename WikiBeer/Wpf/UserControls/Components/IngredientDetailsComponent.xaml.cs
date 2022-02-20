using Ipme.WikiBeer.Models.Ingredients;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UserControls.Components
{
    /// <summary>
    /// Logique d'interaction pour IngredientDetailsComponent.xaml
    /// </summary>
    public partial class IngredientDetailsComponent : UserControl
    {
        public static readonly DependencyProperty IngredientDetailsProperty =
                     DependencyProperty.Register("IngredientDetails", typeof(IngredientModel), typeof(IngredientDetailsComponent));

        public IngredientModel IngredientDetails
        {
            get { return GetValue(IngredientDetailsProperty) as IngredientModel; }
            set
            {
                if (value != null)
                {
                    SetValue(IngredientDetailsProperty, value);
                }
            }
        }
        
        public IngredientDetailsComponent()
        {
            InitializeComponent();
        }

        private void HopRadioButton_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void CerealRadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void AdditiveRadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
