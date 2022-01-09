using BeerServiceReference;
using IngredientServiceReference;
using Ipme.WikiBeer.Persistance;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Ipme.WikiBeer.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
//        public IBeerManager BeerManager { get; } // Pour créer des objets Beer directement
        public BeerServiceClient BeerServiceClient { get; } // pour passer via wcf
        public IngredientServiceClient IngredientServiceClient { get; } // pour passer via wcf

        public App()
        {
            InitializeComponent();
            BeerServiceClient = new BeerServiceClient();
            IngredientServiceClient = new IngredientServiceClient();
//            BeerManager = new BeerManager();
         }
    }
}
