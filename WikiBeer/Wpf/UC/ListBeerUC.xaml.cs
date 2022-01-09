using BeerServiceReference;
using Ipme.WikiBeer.Persistance;
using System.Windows;
using System.Windows.Controls;

namespace Ipme.WikiBeer.Wpf.UC
{
    /// <summary>
    /// Logique d'interaction pour ListBeerUC.xaml
    /// </summary>
    public partial class ListBeerUC : UserControl
    {
//        public IBeerManager BeerManager { get; }
        public BeerServiceClient BeerServiceClient { get; }

        public ListBeerUC()
        {
            InitializeComponent();
            if (Application.Current is App app)
            {
                //                BeerManager = app.BeerManager;
                BeerServiceClient = app.BeerServiceClient;
            }

            var beers = BeerServiceClient.GetBeers();
            BeerList.ItemsSource = beers;
            // BeerListIngredients.ItemSource;
            //BeerList.

            //            BeerServiceClient = 
            //BeerManager = new BeerManager();
            //BeerList.ItemsSource = BeerManager.GetAllBeer();
        }
    }
}
