using Auth0.OidcClient;
using Ipme.WikiBeer.Wpf.UserControls.Views.SecondaryViews;
using Ipme.WikiBeer.Wpf.Utilities;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UserControls.Views
{
    /// <summary>
    /// Logique d'interaction pour HomeU.xaml
    /// </summary>
    public partial class ViewMain : UserControl
    {
        public INavigator Navigator { get; } = new Navigator();

        public Auth0Client AuthClient = ((App)Application.Current).AuthClient;

        public ViewMain()
        {
            InitializeComponent();
            Navigator.RegisterView(new ViewStats());
            Navigator.RegisterView(new ViewBeers());
            Navigator.RegisterView(new ViewBreweries());
            Navigator.RegisterView(new ViewColors());
            Navigator.RegisterView(new ViewStyles());
            Navigator.RegisterView(new ViewIngredients());
            Navigator.RegisterView(new ViewUsers());
            Navigator.RegisterView(new ViewHome());
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

        private void Button_Click_Home(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(ViewHome));
        }

        private void Button_Click_Stats(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(ViewStats));
        }

        private void Button_Click_Beer(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(ViewBeers));
        }

        private void Button_Click_Brewery(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(ViewBreweries));
        }

        private void Button_Click_Ingredient(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(ViewIngredients));
        }

        private void Button_Click_Color(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(ViewColors));
        }

        private void Button_Click_Style(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(ViewStyles));
        }

        private void Button_Click_User(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(ViewUsers));
        }

        private async void Button_Click_Logout(object sender, RoutedEventArgs e) 
        {
            await AuthClient.LogoutAsync();
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.Navigator.NavigateTo(typeof(ViewLogin));
        }

        private void root_Loaded(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(ViewHome));
        }
    }
}
