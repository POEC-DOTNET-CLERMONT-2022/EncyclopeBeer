using Ipme.WikiBeer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Ipme.WikiBeer.Wpf.UC
{
    /// <summary>
    /// Logique d'interaction pour BeerDetails.xaml
    /// </summary>
    public partial class BeerDetailsUC : UserControl
    {
        // DependencyProperty qui sera passée dans les xaml
        private static readonly DependencyProperty CurrentBeerProperty =
            DependencyProperty.Register("CurrentBeer", typeof(BeerModel), typeof(BeerDetailsUC));

        // Doit avoir le même nom que celui définie dans le register
        // Note importante : GetValue et SetValue comprennent le OnNotifiedChange qui fait qu'une DependencyProperty
        // est un observable quoi qu'il arrive
        public BeerModel CurrentBeer
        {
            get { return GetValue(CurrentBeerProperty) as BeerModel; }
            set
            {
                if (CurrentBeer != value)
                {
                    SetValue(CurrentBeerProperty, value);
                }
            }
        }
        public BeerDetailsUC()
        {
            InitializeComponent();
        }


    }
}
