using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.API.Mapping;
using Movies.Application.Services;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;
using MoviesAPI;

namespace Movies.API.Controllers
{
    [ApiController]
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
           

            var result = await _identityService.RegisterAsync(user);
            if (!result.success)
            {
                return BadRequest(new AuthFailedResponse() { ErrorMessages = result.ErrorMessages });
            }


                return Ok(new AuthSuccessResponse() { token = result.token});
        }


        [HttpPost(ApiEndpoints.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = request.MapToUser();
            var result = await _identityService.LoginAsync(user);

            if (!result.success)
            {
                return BadRequest(new AuthFailedResponse() { ErrorMessages = result.ErrorMessages });
            }


            return Ok(new AuthSuccessResponse() { token = result.token });
        }
    }
}
