using System.Collections.Generic;
using AutoFixture;
using Ipme.WikiBeer.Model;

namespace Ipme.WikiBeer.Persistance
{
    public class BeerManager : IBeerManager
    {
        private List<Beer> _beers;

        private readonly Fixture _fixture = new Fixture();

        public BeerManager()
        {
            //Beers = new List<Beer>();
            _beers = new List<Beer>();
            _beers.AddRange(_fixture.CreateMany<Beer>(FixtureDefaultMagic.DEFAULT_BEER_NUMBER));
        }

        public IEnumerable<Beer> GetAllBeer()
        {
            return _beers;
        }
    }
}