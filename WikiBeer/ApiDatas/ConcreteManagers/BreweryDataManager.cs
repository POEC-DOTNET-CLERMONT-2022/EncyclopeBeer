using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;

namespace Ipme.WikiBeer.ApiDatas
{
    public class BreweryDataManager : ApiDataManager<BreweryModel, BreweryDto>
    {
        public BreweryDataManager(HttpClient client, IMapper mapper, string serverUrl)
            : base(client, mapper, serverUrl, "/api/Breweries")
        {
        }
    }
}
