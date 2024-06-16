using mdswebapi.Models;
using mdswebapi.Models.RequestModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace mdswebapi.Controllers
{
    [Route("api/authen")]
    [ApiController]
    public class AuthenControllers : ControllerBase
    {
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized();
        }

        [HttpGet("forbidden")]
        public HttpResponseMessage GetForbidden()
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
        }

        /*[HttpPost("login-cookie")]
        public async Task<IActionResult> LoginCookie([FromBody] Customer userRequestModel)
        {
            string roleName = string.Empty;
            if (userRequestModel.Roles.EndsWith("Admin"))
                roleName = "Admin";
            else if (userRequestModel.Roles.EndsWith("Phar"))
                roleName = "Phar";
            else
                roleName = "Guest";

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userRequestModel.UserName),
                new Claim(ClaimTypes.Role, roleName),
                new Claim("FullName", "Iam someone")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            return Ok("login success");
        }*/

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("Logout success");
        }
    }
}
