using Ipme.WikiBeer.Wpf.Utilities;
using System.Windows;
using System.Windows.Controls;
using Auth0.OidcClient;
using Ipme.WikiBeer.Wpf.UserControls.Views.SecondaryViews;

namespace Ipme.WikiBeer.Wpf.UserControls.Views
{
    /// <summary>
    /// Logique d'interaction pour ViewLogin.xaml
    /// </summary>
    public partial class ViewLogin : UserControl
    {
        public INavigator Navigator { get; set; } = ((App)Application.Current).Navigator;
        private Auth0Client _client { get; set; }


        public ViewLogin()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(ViewHome));
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Auth0ClientOptions clientOptions = new Auth0ClientOptions
            {
                Domain = "domain",
                ClientId = "client_id"
            };
            _client = new Auth0Client(clientOptions);
            clientOptions.PostLogoutRedirectUri = clientOptions.RedirectUri;

            var loginResult = await _client.LoginAsync();

            if (loginResult.TokenResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                Navigator.NavigateTo(typeof(ViewBeers));
            }

        }
    }
}
