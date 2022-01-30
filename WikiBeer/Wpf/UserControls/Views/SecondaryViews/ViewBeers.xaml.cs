using Ipme.WikiBeer.ApiDatas;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;
using Ipme.WikiBeer.Wpf.UserControls.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Ipme.WikiBeer.Wpf.UserControls.Views.SecondaryViews
{
    /// <summary>
    /// Logique d'interaction pour ViewBeers.xaml
    /// </summary>
    public partial class ViewBeers : UserControl
    {
        private IDataManager<BeerModel, BeerDto> _beerDataManager = ((App)Application.Current).BeerDataManager;

        private BeersListModel BeersList { get; set; } = new BeersListModel();

        public ViewBeers()
        {
            LoadBeer();
            InitializeComponent();
        }

        private async void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBeer();
        }

        public async void LoadBeer()
        {
            var beerModels = await _beerDataManager.GetAll();
            BeersList.Beers = new ObservableCollection<BeerModel>(beerModels);
        }
    }
}
