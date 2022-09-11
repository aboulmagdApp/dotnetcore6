using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi.Data.Auth;
using webapi.Dtos.User;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceRespones<string>>> Login(UserLoginDto request)
        {
            var respones = await _authRepo.Login(request.Username, request.Password);
            if (!respones.Success)
            {
                return BadRequest(respones);
            }
            return Ok(respones);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceRespones<int>>> Register(UserRegisterDto request)
        {
            var respones = await _authRepo.Register(
                new User { Username = request.Username }, request.Password
            );
            if (!respones.Success)
            {
                return BadRequest(respones);
            }
            return Ok(respones);
        }
    }
}