using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Ipme.WikiBeer.Model;
using Ipme.WikiBeer.Persistance.Magic;

namespace Ipme.WikiBeer.Persistance
{
    public class BeerManager : IBeerManager
    {
        private List<Beer> _beers;

        private readonly Fixture _fixture = new Fixture();

        public BeerManager()
        {
            // Définitifs
            _beers = new List<Beer>();

            // Fixture
            _beers.AddRange(_fixture.CreateMany<Beer>(FixtureDefaultMagic.DEFAULT_BEER_NUMBER));
        }

        public void AddBeer(Beer beer_to_add)
        {
            _beers.Add(beer_to_add);
        }

        public void DeleteBeer(Beer beer_do_delete)
        {
            if (!_beers.Any())
                _beers.Remove(beer_do_delete);
        }

        public IEnumerable<Beer> GetAllBeer()
        {
            return _beers;
        }
    }
}