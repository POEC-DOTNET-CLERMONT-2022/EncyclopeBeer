//using BeerServiceReference;
//using IngredientServiceReference;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UC
{
    /// <summary>
    /// Logique d'interaction pour HomeU.xaml
    /// </summary>
    public partial class HomeUC : UserControl
    {
        //public IBeerManager BeerManager { get; }
        //public BeerServiceClient BeerServiceClient { get; }
        //public IngredientServiceClient IngredientServiceClient { get; }
        public ListBeerUC ListBeerUC { get; set; } = new ListBeerUC();
        public StatisticUC StatisticUC { get; set; } = new StatisticUC();
        public BreweryUC BreweryUC { get; set; } = new BreweryUC();
        public ListIngredientUC ListIngredientUC { get; set; } = new ListIngredientUC();
        public ColorUC ColorUC { get; set; } = new ColorUC();
        public FamilyUC FamilyUC { get; set; } = new FamilyUC();
        public UserUC UserUC { get; set; } = new UserUC();

        public HomeUC()
        {
            InitializeComponent();
            if (Application.Current is App app)
            {
                //BeerManager = app.BeerManager;
                //BeerServiceClient = app.BeerServiceClient;
                //IngredientServiceClient = app.IngredientServiceClient;
            }
        }

        private void Button_Click_Stats(object sender, RoutedEventArgs e)
        {
            HomeControl.Content = StatisticUC;
        }

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
            HomeControl.Content = ListBeerUC;
        }

        private void Button_Click_Brewery(object sender, RoutedEventArgs e)
        {
            HomeControl.Content = BreweryUC;
        }

        private void Button_Click_Ingredient(object sender, RoutedEventArgs e)
        {
            HomeControl.Content = ListIngredientUC;

        }

        private void Button_Click_Color(object sender, RoutedEventArgs e)
        {
            HomeControl.Content = ColorUC;

        }

        private void Button_Click_Family(object sender, RoutedEventArgs e)
        {
            HomeControl.Content = FamilyUC;
        }

        private void Button_Click_User(object sender, RoutedEventArgs e)
        {
            HomeControl.Content = UserUC;
        }
    }
}
