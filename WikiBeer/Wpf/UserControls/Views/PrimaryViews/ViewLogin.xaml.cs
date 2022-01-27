using Ipme.WikiBeer.Wpf.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Navigator.NavigateTo(typeof(ViewHome));
        }
    }
}
