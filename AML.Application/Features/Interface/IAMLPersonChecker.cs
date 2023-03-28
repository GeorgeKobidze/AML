using AML.Model.Api;
using AML.Model.Api.Request;
using AML.Model.Api.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AML.Application.Features.Interface
{
    public interface IAMLPersonChecker
    {
        Task<ApiServiceResponse<PersonCheckerResponseDTO>> PersonChecker(PersonCheckerRequestDTO personCheckerRequest);
    }
}
