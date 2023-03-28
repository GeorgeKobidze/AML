using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AML.ExternalApi.BaseApiClient
{
    public class BaseApiClient : IBaseApiClient
    {
        private readonly HttpClient _client;

        public BaseApiClient(HttpClient client,string baseUrl)
        {
            _client = client;
            client.BaseAddress = new Uri(baseUrl);
        }
        public async Task<T> CallMethod<T>(string url, object model = null, string method = "GET")
        {
            var response = await  CallApi(url,model,method);
            return await response.Content.ReadAsJsonAsync<T>();
        }

        public async Task<T> CallWithResult<T>(string url, object model) => await CallMethod<T>(url, model, "POST");

        private async Task<HttpResponseMessage> CallApi(string url, object model, string method)
        {
            return method.ToUpper() switch
            {
                "POST" => await _client.PostAsJsonAsync(url, model),
                "PUT" => await _client.PutAsJsonAsync(url, model),
                "DELETE" => await _client.DeleteAsJsonAsync(url),
                _ => await _client.GetAsync(url),
            };
        }
    }
}
