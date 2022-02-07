using Ipme.WikiBeer.Models;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UserControls.Components
{
    /// <summary>
    /// Logique d'interaction pour ColorDetailsComponent.xaml
    /// </summary>
    public partial class StyleDetailsComponent : UserControl
    {
        public static readonly DependencyProperty StyleToModifyProperty =
                    DependencyProperty.Register("StyleToModify", typeof(BeerStyleModel), typeof(StyleDetailsComponent));

        public BeerStyleModel StyleToModify
        {
            get { return GetValue(StyleToModifyProperty) as BeerStyleModel; }
            set
            {
                if (value != null)
                {
                    SetValue(StyleToModifyProperty, value);
                }
            }
        }

        public StyleDetailsComponent()
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
