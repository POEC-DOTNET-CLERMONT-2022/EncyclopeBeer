using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;

namespace Ipme.WikiBeer.ApiDatas
{
    public class ColorDataManager : ApiDataManager<BeerColorModel, BeerColorDto>
    {
        public ColorDataManager(HttpClient client, IMapper mapper, string serverUrl)
            : base(client, mapper, serverUrl, "/api/Colors")
        {
        }
    }
}
