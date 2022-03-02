using Auth0.OidcClient;
using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.Wpf.Utilities;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using Ipme.WikiBeer.Models;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Wpf.UserControls.Views
{
    /// <summary>
    /// Logique d'interaction pour ViewLogin.xaml
    /// </summary>
    public partial class ViewLogin : UserControl
    {
        public INavigator Navigator { get; set; } = ((App)Application.Current).Navigator;
        private UserDataManager _userDataManager = (UserDataManager)((App)Application.Current).UserDataManager;
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
                var sub = loginResult.User.Claims.Where(c => c.Type == "sub").Select(sub => sub.Value).First();
                var connectedUser = await _userDataManager.GetByConnectionId(sub);
                await ContinueOrRetry(connectedUser);
            }
        }

        private async Task ContinueOrRetry(UserModel user)
        {
            if (user.IsCertified)
            {
                Navigator.NavigateTo(typeof(ViewMain));
            }
            else
            {
                var logoutResult = await AuthClient.LogoutAsync();                
            }
        }
    }
}
