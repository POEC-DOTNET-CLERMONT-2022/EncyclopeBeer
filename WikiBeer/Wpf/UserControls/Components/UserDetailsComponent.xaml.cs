using Ipme.WikiBeer.Models;
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

        public UserDetailsComponent()
        {
            InitializeComponent();
        }
    }
}
