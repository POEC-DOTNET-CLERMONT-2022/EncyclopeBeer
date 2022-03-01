using Auth0.OidcClient;
using Ipme.WikiBeer.Wpf.UserControls.Views.SecondaryViews;
using Ipme.WikiBeer.Wpf.Utilities;
using System.Diagnostics;
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


        public Auth0Client AuthClient = ((App)Application.Current).AuthClient;

        public ViewLogin()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var loginResult = await AuthClient.LoginAsync();

            if (loginResult.IsError)
            {
                Debug.WriteLine($"An error occurred during login: {loginResult.Error}");
            }
            else
            {
                Navigator.NavigateTo(typeof(ViewHome));
            }
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
