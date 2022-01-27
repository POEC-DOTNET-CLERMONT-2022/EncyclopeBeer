using Ipme.WikiBeer.Wpf.Utilities;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UC
{
    /// <summary>
    /// Logique d'interaction pour HomeU.xaml ; Ici instanciation d'un autre navigateur dédié aux sous fenêtres
    /// </summary>
    public partial class HomeUC : UserControl
    {
        public ListBeerUC ListBeerUC { get; set; } = new ListBeerUC();
        public StatisticUC StatisticUC { get; set; } = new StatisticUC();
        public BreweryUC BreweryUC { get; set; } = new BreweryUC();
        public ListIngredientUC ListIngredientUC { get; set; } = new ListIngredientUC();
        public ColorUC ColorUC { get; set; } = new ColorUC();
        public FamilyUC FamilyUC { get; set; } = new FamilyUC();
        public UserUC UserUC { get; set; } = new UserUC();

        public INavigator Navigator { get; }
        public HomeUC()
        {
            InitializeComponent();
            // Initialisation du navigateur
            Navigator = new Navigator();
            DataContext = this;
        }

        /// <summary>
        /// Méthode appelée au chargement (voir Loaded = "Onload" dans le xaml)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Onglets principaux
            Navigator.RegisterView(new StatisticUC());
            // il nous manque un écran générale du manager -> peut etre la même chose que sur le coté?
            Navigator.RegisterView(new UserUC());

            // Sous onglet du Manager (bonne idée de charger ici?)
            Navigator.RegisterView(new ListBeerUC());
            Navigator.RegisterView(new BreweryUC());
            Navigator.RegisterView(new ListIngredientUC());
            Navigator.RegisterView(new ColorUC());
            Navigator.RegisterView(new FamilyUC());
        }

        private void Button_Click_Stats(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(StatisticUC));
        }

        /// <summary>
        /// A gérer via du binding plutôt?
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Manager(object sender, RoutedEventArgs e)
        {
            if (SubMenuManager.Visibility != Visibility.Visible)
            {
                SubMenuManager.Visibility = Visibility.Visible;
            }
            else
            {
                SubMenuManager.Visibility = Visibility.Collapsed;
            }

        }

        private void Button_Click_Beer(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(ListBeerUC));
        }

        private void Button_Click_Brewery(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(BreweryUC));
        }

        private void Button_Click_Ingredient(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(ListIngredientUC));
        }

        private void Button_Click_Color(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(ColorUC));
        }

        private void Button_Click_Family(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(FamilyUC));
        }

        private void Button_Click_User(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(UserUC));
        }
    }
}
