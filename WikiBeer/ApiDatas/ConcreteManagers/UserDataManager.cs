using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Ipme.WikiBeer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.ApiDatas
{
    public class UserDataManager : ApiDataManager<UserModel, UserDto>
    {
        public UserDataManager(HttpClient client, IMapper mapper, string serverUrl)
            : base(client, mapper, serverUrl, "/api/Users")
        {
        }

        public async Task<IEnumerable<BeerModel>> GetFavoriteBeersById(Guid id)
        {
            var response = await Client.GetAsync(Uri.AbsoluteUri + $"/{id}/favoriteBeers");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<IEnumerable<BeerDto>>(responseString,
                JsonSerializerSettings);

            return Mapper.Map<IEnumerable<BeerModel>>(dto);
        }
    }
}
