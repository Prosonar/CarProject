using Business.Abstract.Services;
using Core.Entity.Concrete;
using Core.Utilities.Security.Hashing;
using Entity.Concrete;
using Entity.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IAuthorizationService _authorizationService;

        public UserController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegister userForRegister)
        {
            var userToRegister = _authorizationService.Register(userForRegister);
            if(!userToRegister.Success)
            {
                return BadRequest(userToRegister.Message);
            }
            var accessToken = _authorizationService.CreateAccessToken(userToRegister.Data);
            if(accessToken.Success)
            {
                return Ok(accessToken.Data);
            }
            return BadRequest("Token üretilemedi.");
        }
        [HttpPost("login")]
        public IActionResult Login(UserForLogin userForLogin)
        {
            var userToLogin = _authorizationService.Login(userForLogin);
            if(!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            var accessToken = _authorizationService.CreateAccessToken(userToLogin.Data);
            if(accessToken.Success)
            {
                return Ok(accessToken.Data);
            }
            return BadRequest("Token üretilemedi.");
        }
    }
}
