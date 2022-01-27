using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.Models
{
    public class BeersListModel : ObservableObject
    {
        private ObservableCollection<BeerModel> beers;

        private BeerModel currentBeer;

        private BeerModel beerToModify;

        public BeerModel BeerToModify
        {
            get
            {
                return beerToModify;
            }
            set
            {
                beerToModify = value;
                OnNotifyPropertyChanged();
            }
        }

        public ObservableCollection<BeerModel> Beers
        {
            get { return beers; }
            set
            {
                if (beers != value)
                {
                    beers = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        public BeerModel CurrentBeer
        {
            get { return currentBeer; }
            set
            {
                if (currentBeer != value)
                {
                    currentBeer = value;
                    OnNotifyPropertyChanged();
                    BeerToModify = new BeerModel(CurrentBeer);
                }
            }
        }
    }
}
