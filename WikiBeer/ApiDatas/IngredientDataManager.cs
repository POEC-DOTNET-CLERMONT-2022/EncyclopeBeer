using AutoMapper;
using Ipme.WikiBeer.Dtos.Ingredients;
using Ipme.WikiBeer.Models.Ingredients;
using Newtonsoft.Json;

namespace Ipme.WikiBeer.ApiDatas
{
    public class IngredientDataManager : ApiDataManager<IngredientModel, IngredientDto>
    {
        public IngredientDataManager(HttpClient client, IMapper mapper, string serverUrl)
            : base(client, mapper, serverUrl, "/api/Ingredients")
        {
        }
    }
}
