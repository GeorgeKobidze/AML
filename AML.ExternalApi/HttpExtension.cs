using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AML.ExternalApi
{
    public static class HttpExtension
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync(
           this HttpClient httpClient, string url, object data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return httpClient.PostAsync(url, content);
        }

        public static Task<HttpResponseMessage> PutAsJsonAsync(
            this HttpClient httpClient, string url, object data)
        {

            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpClient.PutAsync(url, content);
        }

        public static Task<HttpResponseMessage> DeleteAsJsonAsync(
         this HttpClient httpClient, string url)
        {        
            return httpClient.DeleteAsync(url);
        }

        public static Task<HttpResponseMessage> GetAsync(
          this HttpClient httpClient, string url)
        {
            httpClient.DefaultRequestHeaders.Clear();
            
            return httpClient.GetAsync(url);
        }

        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var dataAsString = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(dataAsString);
        }

    }
}
