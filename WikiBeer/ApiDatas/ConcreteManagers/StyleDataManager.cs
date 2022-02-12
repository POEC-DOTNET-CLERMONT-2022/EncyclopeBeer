using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;

namespace Ipme.WikiBeer.ApiDatas
{
    public class StyleDataManager : ApiDataManager<BeerStyleModel, BeerStyleDto>
    {
        public StyleDataManager(HttpClient client, IMapper mapper, string serverUrl)
            : base(client, mapper, serverUrl, "/api/Styles")
        {
        }
    }
}
