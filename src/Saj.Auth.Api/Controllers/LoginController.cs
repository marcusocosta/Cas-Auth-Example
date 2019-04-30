using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Saj.Auth.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        [HttpGet("access-denied")]
        public string AccessDenied()
        {
            return "Não autorizado";
        }

        [AllowAnonymous]
        [HttpGet("login")]
        public async Task Login(string returnUrl)
        {
            var request = Request.Cookies;
            
            var props = new AuthenticationProperties { RedirectUri = returnUrl };
            await HttpContext.ChallengeAsync("CAS", props);
        }
        
        [AllowAnonymous]
        [HttpGet("signin")]
        public string Signin()
        {
            return "";
        }
        
        [Authorize]
        [HttpGet("tokens")]
        public string Tokens()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            
            
            var request = Request.Cookies;
            return "";
        }
        
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            
            return Redirect($"{_configuration["CasBaseUrl"]}/logout");
            
        }
    }
}