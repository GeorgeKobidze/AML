using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AML.ExternalApi.BaseApiClient
{
    public interface IBaseApiClient
    {
        Task<T> CallMethod<T>(string url, object model = null, string method = "GET");
        Task<T> CallWithResult<T>(string url, object model);
    }
}
