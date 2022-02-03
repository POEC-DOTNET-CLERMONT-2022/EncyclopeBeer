//using System.Collections.ObjectModel;

//namespace Ipme.WikiBeer.Models
//{
//    public class BeersListModel : ObservableObject
//    {
//        private ObservableCollection<BeerModel> beers;

//        private BeerModel currentBeer;

//        private BeerModel beerToModify;

//        public ObservableCollection<BeerModel> Beers
//        {
//            get { return beers; }
//            set
//            {
//                if (beers != value)
//                {
//                    beers = value;
//                    OnNotifyPropertyChanged();
//                }
//            }
//        }

//        public BeerModel CurrentBeer
//        {
//            get { return currentBeer; }
//            set
//            {
//                if (currentBeer != value)
//                {
//                    currentBeer = value;
//                    OnNotifyPropertyChanged();
//                    BeerToModify = new BeerModel(CurrentBeer);
//                }
//            }
//        }

//        public BeerModel BeerToModify
//        {
//            get
//            {
//                return beerToModify;
//            }
//            set
//            {
//                beerToModify = value;
//                OnNotifyPropertyChanged();
//            }
//        }
//    }
//}
