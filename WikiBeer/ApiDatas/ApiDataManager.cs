using AutoMapper;
using Ipme.WikiBeer.Dtos.SerializerSettings;
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
        protected HttpClient Client { get; }
        protected IMapper Mapper { get; }
        private string ServerUrl { get; }
        private string ResourceUrl { get; }
        protected Uri Uri { get; }        
        protected JsonSerializerSettings JsonSerializerSettings { get; set; }

        public ApiDataManager(HttpClient client, IMapper mapper, string serverUrl, string resourceUrl)
        {
            Client = client;
            Mapper = mapper;
            ServerUrl = serverUrl;
            ResourceUrl = resourceUrl;
            Uri = new Uri(ServerUrl + ResourceUrl);
            JsonSerializerSettings = DtoSettings.DefaultSettings;
        }

        public virtual async Task<TModel> Add(TModel model)
        {
            var dto = Mapper.Map<TDto>(model);
            var dtoString = JsonConvert.SerializeObject(dto, JsonSerializerSettings);

            var postRequest = new HttpRequestMessage(HttpMethod.Post, Uri.AbsoluteUri);
            postRequest.Headers.Add("Accept", "*/*");
            postRequest.Content = new StringContent(dtoString, System.Text.Encoding.UTF8, "application/json-patch+json");
            
            var response = await Client.SendAsync(postRequest);
                      
            var responseString = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<TDto>(responseString,
                JsonSerializerSettings);
            
            return Mapper.Map<TModel>(responseDto);
        }

        public virtual async Task<IEnumerable<TModel>> GetAll()
        {
            var response = await Client.GetAsync(Uri.AbsoluteUri);
            response.EnsureSuccessStatusCode(); 

            var responseString = await response.Content.ReadAsStringAsync();
            var dtos = JsonConvert.DeserializeObject<IEnumerable<TDto>>(responseString,
                JsonSerializerSettings);
            
            return Mapper.Map<IEnumerable<TModel>>(dtos);
        }

        public virtual async Task<TModel> GetById(Guid id)
        {
            var response = await Client.GetAsync(Uri.AbsoluteUri+$"/{id}");
            response.EnsureSuccessStatusCode(); 

            var responseString = await response.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<TDto>(responseString,
                JsonSerializerSettings);
            
            return Mapper.Map<TModel>(dto);
        }

        public virtual async Task<TModel> Update(Guid id, TModel model)
        {
            var dto = Mapper.Map<TDto>(model);
            var dtoString = JsonConvert.SerializeObject(dto, JsonSerializerSettings);

            var putRequest = new HttpRequestMessage(HttpMethod.Put, Uri.AbsoluteUri+$"/{id}");
            putRequest.Headers.Add("Accept", "*/*");
            putRequest.Content = new StringContent(dtoString, System.Text.Encoding.UTF8, "application/json-patch+json");
            var response = await Client.SendAsync(putRequest);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<TDto>(responseString,
                JsonSerializerSettings);

            return Mapper.Map<TModel>(responseDto);
        }

        public virtual async Task DeleteById(Guid id)
        {
            var response = await Client.DeleteAsync(Uri.AbsoluteUri+$"/{id}");
            response.EnsureSuccessStatusCode();
            //var responseString = await response.Content.ReadAsStringAsync();
            //var success = JsonConvert.DeserializeObject<bool>(responseString,
            //    JsonSerializerSettings);
            //return success;
        }
    }
}
