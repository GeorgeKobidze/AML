using AML.ExternalApi.BaseApiClient;
using AML.Model.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AML.ExternalApi.ApiClient
{
    public interface ITronApiClient : IBaseApiClient
    {
    }

    public class TronApiClient : BaseApiClient.BaseApiClient, ITronApiClient
    {
        public TronApiClient(HttpClient client,IOptions<ApiUrl> apiUrl) : base(client, apiUrl.Value.TronApiUrl)
        {
        }
    }
}
