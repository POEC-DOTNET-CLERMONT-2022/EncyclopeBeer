using Ipme.WikiBeer.Models.Ingredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ipme.WikiBeer.Wpf.UserControls.Components
{
    /// <summary>
    /// Logique d'interaction pour IngredientDetailsComponent.xaml
    /// </summary>
    public partial class IngredientDetailsComponent : UserControl
    {
        public static readonly DependencyProperty IngredientToModifyProperty =
                     DependencyProperty.Register("IngredientToModify", typeof(IngredientModel), typeof(IngredientDetailsComponent));

        public IngredientModel IngredientToModify
        {
            get { return GetValue(IngredientToModifyProperty) as IngredientModel; }
            set
            {
                if (value != null)
                {
                    SetValue(IngredientToModifyProperty, value);
                }
            }
        }
        
        public IngredientDetailsComponent()
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
