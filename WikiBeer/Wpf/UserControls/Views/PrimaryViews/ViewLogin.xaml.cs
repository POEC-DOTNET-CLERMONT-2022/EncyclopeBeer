﻿using Auth0.OidcClient;
using Ipme.WikiBeer.Wpf.UserControls.Views.SecondaryViews;
using Ipme.WikiBeer.Wpf.Utilities;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UserControls.Views
{
    /// <summary>
    /// Logique d'interaction pour ViewLogin.xaml
    /// </summary>
    public partial class ViewLogin : UserControl
    {
        public INavigator Navigator { get; set; } = ((App)Application.Current).Navigator;

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
    }
}
