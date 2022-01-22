using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipme.WikiBeer.ApiDatas
{
    public class ApiDataManager<TModel, TDto> : IDataManager<TModel, TDto> where TModel : class where TDto : class
    {
        private HttpClient Client { get; }
        private IMapper Mapper { get; }
        private string ServerUrl { get; }
        private string ResourceUrl { get; }

        private Uri Uri { get; }

        public ApiDataManager(HttpClient client, IMapper mapper, string serverUrl, string resourceUrl)
        {
            Client = client;
            Mapper = mapper;
            ServerUrl = serverUrl;
            ResourceUrl = resourceUrl;
            Uri = new Uri(ServerUrl + ResourceUrl);
        }

        public async Task<IEnumerable<TModel>> GetAll()
        {
            //var request = new HttpRequestMessage(HttpMethod.Get, Uri);
            //request.Headers.Add("Accept", "application/json");
            //var response = await Client.SendAsync(request);
            var response = await Client.GetAsync(Uri);
            response.EnsureSuccessStatusCode(); // pète une exceptrion en cas de problème
            // Sérialisation 
            var responseString = await response.Content.ReadAsStringAsync();
            var dtos = JsonConvert.DeserializeObject<IEnumerable<BeerDto>>(responseString,
                GetJsonSettings());
            //var result = await HttpClient.GetFromJsonAsync<IEnumerable<TDto>>(Uri);
            return Mapper.Map<IEnumerable<TModel>>(dtos);
        }

        public async Task Add(TModel model)
        {
            var dto = Mapper.Map<TDto>(model);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, Uri);
            postRequest.Headers.Add("Accept", "*/*"); // header
                                                      //postRequest.Headers.Add("Content-Type", "application/json-patch+json");
            var dtoString = JsonConvert.SerializeObject(dto, GetJsonSettings());
            // Encoding et application application/json-patch+json nécessaire pour que la requête passes
            postRequest.Content = new StringContent(dtoString, System.Text.Encoding.UTF8, "application/json-patch+json");
            var response = await Client.SendAsync(postRequest);
            //await HttpClient.PostAsJsonAsync(Uri, dto);
        }

        protected JsonSerializerSettings GetJsonSettings()
        {
            return new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
        }

    }
}
