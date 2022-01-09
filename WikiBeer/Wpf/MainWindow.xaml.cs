using BeerServiceReference;
using Ipme.WikiBeer.Persistance;
using Ipme.WikiBeer.Wpf.UC;
using System.Windows;

namespace Ipme.WikiBeer.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
//        public IBeerManager BeerManager { get; }
        public BeerServiceClient BeerServiceClient { get; }
        public HomeUC HomeUC { get; set; } = new HomeUC();

        public MainWindow()
        {
            InitializeComponent();
            if (Application.Current is App app)
            {
 //               BeerManager = app.BeerManager;
                  BeerServiceClient = app.BeerServiceClient;
            }

            MainControl.Content = HomeUC;
        }
    }
}
