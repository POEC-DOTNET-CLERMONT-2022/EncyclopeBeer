using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Persistance;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UC
{
    /// <summary>
    /// Logique d'interaction pour ListBeerUC.xaml
    /// </summary>
    public partial class ListBeerUC : UserControl
    {
        private readonly IDataManager<BeerModel, BeerDto> _beerDataManager;
        public BeersList BeersList { get; set; } = new BeersList();

        private IEnumerable<BeerModel> Beer;

        public ListBeerUC()
        {
            InitializeComponent();
            if (Application.Current is App app)
            {
                _beerDataManager = app.BeerDataManager;
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Beer = await _beerDataManager.GetAll();
            BeersList.Beers = new ObservableCollection<BeerModel>(Beer);

            DataContext = BeersList;
        }

        private void Update_Beer_Click(object sender, RoutedEventArgs e)
        {
            //var beer = BeersList.Beers.FirstOrDefault(beers => beers.Id == BeersList.CurrentBeer.Id);
            //BeersList.Beers.Add(BeersList.BeerToModify);

            //if (beer != null)
            //{
            //    BeersList.Beers.Remove(beer);
            //}
        }

        private void Get_Beer_List_Click(object senfer, RoutedEventArgs e)
        {
            //var beerModels = _mapper.Map<IEnumerable<BeerModel>>(_beerRepository.GetAll());
            //BeersList.Beers = new ObservableCollection<BeerModel>(beerModels);
        }

        private void ListBeers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
