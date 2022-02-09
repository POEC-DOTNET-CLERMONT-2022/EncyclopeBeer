using AutoMapper;
using Ipme.WikiBeer.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Liens utiles pour les méthode de HttpClient : 
/// https://docs.microsoft.com/fr-fr/dotnet/api/system.net.http.httpclient?view=net-6.0
/// TODO : retravailler update et add pour utiliser driectement Client.PostAsync et Client.PutAsync
/// </summary>
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

        public virtual async Task Add(TModel model)
        {
            var dto = Mapper.Map<TDto>(model);
            var dtoString = JsonConvert.SerializeObject(dto, GetJsonSettings());

            var postRequest = new HttpRequestMessage(HttpMethod.Post, Uri.AbsoluteUri);
            postRequest.Headers.Add("Accept", "*/*");
            postRequest.Content = new StringContent(dtoString, System.Text.Encoding.UTF8, "application/json-patch+json");
            
            var response = await Client.SendAsync(postRequest);
            
            response.EnsureSuccessStatusCode(); // pète une exception en cas de problème
            //await HttpClient.PostAsJsonAsync(Uri, dto);
        }

        public virtual async Task<IEnumerable<TModel>> GetAll()
        {
            //var request = new HttpRequestMessage(HttpMethod.Get, Uri);
            //request.Headers.Add("Accept", "application/json");
            //var response = await Client.SendAsync(request);
            var response = await Client.GetAsync(Uri.AbsoluteUri);
            response.EnsureSuccessStatusCode(); // pète une exception en cas de problème
            // Sérialisation 
            var responseString = await response.Content.ReadAsStringAsync();
            var dtos = JsonConvert.DeserializeObject<IEnumerable<TDto>>(responseString,
                GetJsonSettings());
            //var result = await HttpClient.GetFromJsonAsync<IEnumerable<TDto>>(Uri);
            return Mapper.Map<IEnumerable<TModel>>(dtos);
        }

        public virtual async Task<TModel> GetById(Guid id)
        {
            var response = await Client.GetAsync(Uri.AbsoluteUri+$"/{id}");
            response.EnsureSuccessStatusCode(); // pète une exception en cas de problème
            // Sérialisation 
            var responseString = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<TDto>(responseString,
                GetJsonSettings());
            
            return Mapper.Map<TModel>(dto);
        }

        public virtual async Task Update(Guid id, TModel model)
        {
            var dto = Mapper.Map<TDto>(model);
            var dtoString = JsonConvert.SerializeObject(dto, GetJsonSettings());

            var putRequest = new HttpRequestMessage(HttpMethod.Put, Uri.AbsoluteUri+$"/{id}");
            putRequest.Headers.Add("Accept", "*/*");
            putRequest.Content = new StringContent(dtoString, System.Text.Encoding.UTF8, "application/json-patch+json");
            var response = await Client.SendAsync(putRequest);
            response.EnsureSuccessStatusCode(); 
        }

        public virtual async Task<bool> DeleteById(Guid id)
        {
            var response = await Client.DeleteAsync(Uri.AbsoluteUri+$"/{id}");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var success = JsonConvert.DeserializeObject<bool>(responseString,
                GetJsonSettings());
            return success;
        }
 
        protected virtual JsonSerializerSettings GetJsonSettings()
        {
            return new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        }

    }
}
