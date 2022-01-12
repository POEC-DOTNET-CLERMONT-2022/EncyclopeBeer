using IngredientServiceReference;
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

namespace Ipme.WikiBeer.Wpf.UC
{
    /// <summary>
    /// Logique d'interaction pour ListIngredientUC.xaml
    /// </summary>
    public partial class ListIngredientUC : UserControl
    {
        public IngredientServiceClient IngredientServiceClient { get; }
        public ListIngredientUC()
        {
            InitializeComponent();
            if (Application.Current is App app)
            {
                IngredientServiceClient = app.IngredientServiceClient;
            }

            var ingredients = IngredientServiceClient.GetIngredients();
            // TODO retransformation en véritable objet ici

            HopList.ItemsSource = ingredients; // TODO : A splitter en Hops, Cereal et Additive -> De nouveaux DTO pour sa (à l'étape précédente). Voir comment passer des objets hérités via DTO
            IngredientServiceClient.Close();
        }
    }
}
