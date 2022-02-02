using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;

namespace Ipme.WikiBeer.ApiDatas
{
    public class CountryDataManager : ApiDataManager<CountryModel, CountryDto>
    {
        public CountryDataManager(HttpClient client, IMapper mapper, string serverUrl)
            : base(client, mapper, serverUrl, "/api/Countries")
        {
        }
    }
}
