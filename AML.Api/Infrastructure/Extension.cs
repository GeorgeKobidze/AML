using AML.Model.Api;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.NetworkInformation;

namespace AML.Api.Infrastructure
{
    public static class Extension
    {
        public static ObjectResult ResponseResult(this ControllerBase controller, ApiServiceResponse response)
        {
            switch (response.State)
            {
                case ApiStatus.Ok:
                    return controller.Ok(response);
                case ApiStatus.NotFound:
                    return controller.NotFound(response);
                case ApiStatus.Failed:
                    return controller.StatusCode((int)HttpStatusCode.InternalServerError, response);
                case ApiStatus.AlreadyExists:
                    return controller.StatusCode((int)HttpStatusCode.Conflict, response);
                default:
                    return controller.BadRequest(response);
            }
        }
    }
}
