using Application.Domain.Models;
using Application.Hosts.Api.AuthenticationManager;
using Application.Hosts.Api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web.Mvc.Html;
using static Application.Hosts.Api.Models.UserAccess;

namespace Application.Hosts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public LoginController(IAuthManager authManager)
        {
            _authManager = authManager;

        }

        // POST api/<LoginController>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Post([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!(loginRequest.UserId == "a" && loginRequest.Password == "a"))
            {
                return Ok(new { Response = "Incorrect Details" });
            }

            var user = new SystemUser();
            user.Id = 1;
            user.UserName = "Gnyasha";


            var claims = new ClaimsIdentity(new[] {
                         new Claim(ClaimTypes.Name, user.Id.ToString()),
                         new Claim(ClaimTypes.NameIdentifier,user.UserName),
                         new Claim(ClaimTypes.Role, "Admin")
            });

            //int roleId = Convert.ToInt32(EnumHelper<UserRoles>.GetValueFromName(info.UserRole.ToString()));
            List<UserModuleAccess> pageAccess = new List<UserModuleAccess>();
            if (user.RoleId == 1)
            {
                UserModuleAccess access = new UserModuleAccess();
                access.active = true;
                access.IsAdd = true;
                access.IsDelete = true;
                access.IsEdit = true;
                access.IsView = true;
                access.RoleId = 1;
                pageAccess.Add(access);
            }
            else
            {
                UserModuleAccess access = new UserModuleAccess();
                access.active = true;
                access.IsAdd = true;
                access.IsDelete = false;
                access.IsEdit = true;
                access.IsView = true;
                access.RoleId = 2;
                pageAccess.Add(access);
            }

            foreach (var item in pageAccess)
            {
                claims.AddClaim(new Claim("Home", item.IsView + "|" + item.IsAdd + "|" + item.IsEdit + "|" + item.IsDelete));
            }
            var claimsPrincipal = new ClaimsPrincipal(claims);

            var authResult = _authManager.GenerateTokens(loginRequest.UserId, claimsPrincipal.Claims.ToArray(), DateTime.Now);

            return Ok(new LoginResult
            {
                UserId = loginRequest.UserId,
                AccessToken = authResult.AccessToken,
                RefreshToken = authResult.RefreshToken
            });
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var username = User.Identity.Name;

                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    return Unauthorized();
                }
                var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
                var authResult = _authManager.Refresh(request.RefreshToken, accessToken, DateTime.Now);
                return Ok(new LoginResult
                {
                    UserId = username,
                    AccessToken = authResult.AccessToken,
                    RefreshToken = authResult.RefreshToken

                });
            }
            catch (SecurityTokenException e)
            {
                throw;
            }
        }

    }

    public class RefreshTokenRequest
    {
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }

}
