using WikiBeer.ConsoleApp.Beers;

namespace WikiBeer.ConsoleApp
{
    internal class BeerManager
    {
        private readonly List<Beer> _beers;

        public BeerManager()
        {
            _beers = new List<Beer>();
        }

        public void AddBeer(Beer beerToAdd)
        {
            _beers.Add(beerToAdd);
        }

        public IEnumerable<Beer> Beers
        {
            get { return _beers.AsReadOnly(); }
        }

        public Beer GetBeerByName(string beerName)
        {
            // Ajouter foncion
            Beer beer = _beers.Where(beer => beer.Name == beerName).First();
            return beer;
        }

        public Beer GetBeerById(Guid guid)
        {
            // Ajouter foncion
            Beer beer = _beers.Where(beer => beer.Id == guid).First();
            return beer;
        }


        public void UpdateBeer(Beer beerUpdated)
        {
            Beer originalBeer = GetBeerById(beerUpdated.Id);

            if (beerUpdated != originalBeer)
            {
                // Taratata toto putain d'enum
            }
            else
            {
                throw new Exception("No change !");
            }
        }

        public void DeleteBeer(Guid guid)
        {
            _beers.Remove(_beers.Where(beer => beer.Id == guid).First());
        }
    }
}
