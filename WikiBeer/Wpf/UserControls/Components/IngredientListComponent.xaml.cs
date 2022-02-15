using Ipme.WikiBeer.Models;
using System.Windows;
using System.Windows.Controls;

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
