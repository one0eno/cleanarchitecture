using CleanArchitecture.Application.Contracts.Identity;
using CleanArchitecture.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController:ControllerBase
    {
        private readonly IAuthService _authservice;

        public AccountController(IAuthService authservice)
        {
            _authservice = authservice;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
        {

            return Ok(await _authservice.Login(request));

        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegistrationResponse>> Register([FromBody] RegistrationRequest  request)
        {

            return Ok(await _authservice.Register(request));

        }

    }
}
