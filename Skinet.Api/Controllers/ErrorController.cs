using Microsoft.AspNetCore.Mvc;
using Skinet.Api.Errors;

namespace Skinet.Api.Controllers
{
    [Route("errors/{code}")]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
