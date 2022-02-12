using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;

namespace Ipme.WikiBeer.ApiDatas
{
    public class BeerDataManager : ApiDataManager<BeerModel, BeerDto>
    {
        public BeerDataManager(HttpClient client, IMapper mapper, string serverUrl)
            : base(client, mapper, serverUrl, "/api/Beers")
        {
        }
    }
}
