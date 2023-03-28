using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AML.Model.Api
{
    public enum ApiStatus
    {
        Ok,
        NotFound,
        Failed,
        BadRequest,
        AlreadyExists
    }

    public abstract class ApiServiceResponse
    {
        public string Message { get; set; }
        public ApiStatus State { get; set; }
        public string ErrorCode { get; set; }
        public List<string> ValidationErrors { get; set; }
        public bool IsOk() => State == ApiStatus.Ok;

    }

    public class ApiServiceResponse<T> : ApiServiceResponse
    {
        public ApiServiceResponse() { }
        public ApiServiceResponse(T data, ApiServiceResponse response)
        {
            base.ErrorCode = response.ErrorCode;
            base.Message = response.Message;
            base.State = response.State;
            base.ValidationErrors = response.ValidationErrors;
            Data = data;
        }
        public T Data { get; protected set; }
    }



    public class SuccessApiServiceResponse : ApiServiceResponse
    {
        public SuccessApiServiceResponse(string message = null)
        {
            State = ApiStatus.Ok;
            Message = message;
        }
    }

    public class SuccessApiServiceResponse<T> : ApiServiceResponse<T>
    {
        public SuccessApiServiceResponse(T data, string message = null, bool isAuth = false)
        {
            Data = data;
            State = ApiStatus.Ok;
            Message = message;
        }
    }


    public class BadRequestApiServiceResponse<T> : ApiServiceResponse<T>
    {
        public BadRequestApiServiceResponse(T data, string message = null, string errorCode = "BAD_REQUEST", List<string> validationErrors = null, bool isAuth = false)
        {
            ErrorCode = errorCode;
            State = ApiStatus.BadRequest;
            Message = message;
            Data = data;
            ValidationErrors = validationErrors;
        }
    }

    public class BadRequestApiServiceResponse : ApiServiceResponse
    {
        public BadRequestApiServiceResponse(string message = null, string errorCode = "BAD_REQUEST")
        {
            ErrorCode = errorCode;
            State = ApiStatus.BadRequest;
            Message = message;
        }
    }
}
