using Ipme.WikiBeer.Wpf.Utilities;
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

        public ViewLogin()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(ViewHome));
        }
    }
}
