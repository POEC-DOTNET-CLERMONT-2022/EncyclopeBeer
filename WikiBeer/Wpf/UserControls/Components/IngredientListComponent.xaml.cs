using Ipme.WikiBeer.Models;
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
    /// Logique d'interaction pour IngredientListComponent.xaml
    /// </summary>
    public partial class IngredientListComponent : UserControl
    {
        public static readonly DependencyProperty DataListProperty =
            DependencyProperty.Register("DataList", typeof(GenericListModel<Models.Ingredients.IngredientModel>), typeof(IngredientListComponent));

        // La DepencyProperty renvoie forcément une property qui est Observable
        // par construction donc ce n'est pas la peine de lui affecter IObservable
        public GenericListModel<Models.Ingredients.IngredientModel> DataList
        {
            get { return GetValue(DataListProperty) as GenericListModel<Models.Ingredients.IngredientModel>; }
            set
            {
                if (value != null)
                {
                    SetValue(DataListProperty, value);
                }
            }
        }

        public IngredientListComponent()
        {
            InitializeComponent();
        }
    }
}
