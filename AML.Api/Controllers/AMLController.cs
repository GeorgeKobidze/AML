using AML.Api.Infrastructure;
using AML.Application.Features.Interface;
using AML.Model.Api;
using AML.Model.Api.Request;
using AML.Model.Api.Response;
using Microsoft.AspNetCore.Mvc;

namespace AML.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AMLController : ControllerBase
    {
        private readonly IAMLPersonChecker _aMLPersonChecker;

        public AMLController(IAMLPersonChecker aMLPersonChecker)
        {
            _aMLPersonChecker = aMLPersonChecker;
        }


        [HttpPost]
        public async Task<ActionResult<ApiServiceResponse<PersonCheckerResponseDTO>>> PersonChecker([FromBody] PersonCheckerRequestDTO personChecker)
             => this.ResponseResult(await _aMLPersonChecker.PersonChecker(personChecker));
    }
}
