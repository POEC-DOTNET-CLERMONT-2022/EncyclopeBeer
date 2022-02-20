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
        //private IDataManager<BeerStyleModel, BeerStyleDto> _styleDataManager = ((App)Application.Current).StyleDataManager;

        public static readonly DependencyProperty UserDetailsProperty =
                    DependencyProperty.Register("UserDetails", typeof(UserModel), typeof(StyleDetailsComponent));

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
