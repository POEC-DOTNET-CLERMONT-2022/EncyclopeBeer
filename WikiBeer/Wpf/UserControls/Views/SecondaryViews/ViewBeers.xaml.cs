using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UserControls.Views.SecondaryViews
{
    /// <summary>
    /// Logique d'interaction pour ViewBeers.xaml
    /// </summary>
    public partial class ViewBeers : UserControl
    {
        private IDataManager<BeerModel, BeerDto> _beerDataManager = ((App)Application.Current).BeerDataManager;

        public BeersListModel BeersList { get; set; } 

        public ViewBeers()
        {
            BeersList = new BeersListModel();
            InitializeComponent();
        }

        public async void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadBeers();
        }

        public async Task LoadBeers()
        {
            var beerModels = await _beerDataManager.GetAll();
            BeersList.Beers = new ObservableCollection<BeerModel>(beerModels);
        }
    }
}
