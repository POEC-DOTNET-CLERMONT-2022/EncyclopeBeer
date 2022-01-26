//using BeerServiceReference;
//using Ipme.WikiBeer.Persistance;
using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Wpf.UC;
using System.Collections.Generic;
using System.Windows;

namespace Ipme.WikiBeer.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDataManager<BeerModel, BeerDto> _beerDataManager
            = ((App)Application.Current).BeerDataManager;
        public HomeUC HomeUC { get; set; } = new HomeUC();

        public IEnumerable<BeerModel> Beers { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            MainControl.Content = HomeUC;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Beers = await _beerDataManager.GetAll();

        }
    }
}
