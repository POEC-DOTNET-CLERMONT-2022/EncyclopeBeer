//using BeerServiceReference;
//using Ipme.WikiBeer.Persistance;
using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Wpf.UC;
using Ipme.WikiBeer.Wpf.Utilities;
using System.Collections.Generic;
using System.Windows;

namespace Ipme.WikiBeer.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Récupération du navigateur globale de l'application
        public INavigator Navigator { get; set; } = ((App) Application.Current).Navigator;

        public MainWindow()
        {
            InitializeComponent();
            Navigator.NavigateTo(typeof(HomeUC));
        }
    }
}
