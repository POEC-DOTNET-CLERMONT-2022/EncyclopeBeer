using Ipme.WikiBeer.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UserControls.Components
{
    /// <summary>
    /// Logique d'interaction pour UserDetailsComponent.xaml
    /// </summary>
    public partial class UserDetailsComponent : UserControl
    {
        public static readonly DependencyProperty UserDetailsProperty =
            DependencyProperty.Register("UserDetails", typeof(UserModel), typeof(UserDetailsComponent));

        public UserModel UserDetails
        {
            get { return GetValue(UserDetailsProperty) as UserModel; }
            set
            {
                if (value != null)
                {
                    SetValue(UserDetailsProperty, value);
                }
            }
        }
        
        public static readonly DependencyProperty FavoriteBeersProperty =
            DependencyProperty.Register("FavoriteBeers", typeof(ObservableCollection<BeerModel>), typeof(UserDetailsComponent));

        public ObservableCollection<BeerModel> FavoriteBeers
        {
            get { return GetValue(FavoriteBeersProperty) as ObservableCollection<BeerModel>; }
            set
            {
                if (value != null)
                {
                    SetValue(FavoriteBeersProperty, value);
                }
            }
        }

        public UserDetailsComponent()
        {
            InitializeComponent();
        }

        private void UpgradeUserRole(object sender, RoutedEventArgs e)
        {
            UserDetails.IsCertified = !UserDetails.IsCertified!;
        }
    }
}
