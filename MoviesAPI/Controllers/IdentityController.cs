using Microsoft.AspNetCore.Mvc;
using Movies.API.Mapping;
using Movies.Application.Services;
using Movies.Contracts.Requests;
using MoviesAPI;

namespace Movies.API.Controllers
{
    public class IdentityController : ControllerBase
    {

        private readonly IIdentityService _identityService;
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        [HttpPost(ApiEndpoints.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegisterationRequest request)
        {
            var user = request.MapToUser();
            var result = _identityService.RegisterAsync(user);
            return Ok();
        }
    }
}
