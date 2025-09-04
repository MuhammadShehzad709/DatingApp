using DatingApi.Dtos.AuthenticationDtos;
using DatingApi.Services.AuthenticationServices;
using Microsoft.AspNetCore.Mvc;

namespace DatingApi.Controllers
{
    public class accountController(IAuthenticationService service):BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult>Register(RegistorDto registorDto)
        {          
            return Ok(await service.Register(registorDto));
        }
        [HttpPost("login")]
        public async Task<IActionResult>login(LoginDto loginDto)
        {
            return Ok(await service.Login(loginDto));
        }
        
    }
}
