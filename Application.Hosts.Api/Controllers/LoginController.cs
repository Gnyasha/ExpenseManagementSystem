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


namespace Application.Hosts.Api.Controllers
{
    using Application.Contracts.DatabaseSessions;
    using Application.Domain.Models;
    using Application.Hosts.Api.AuthenticationManager;
    using Application.Hosts.Api.Models;
    using static Application.Hosts.Api.Models.UserAccess;

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthManager authManager;
        private readonly IDbAccess dbAccess;

        public LoginController(IAuthManager _authManager, IDbAccess _dbAccess)
        {
            authManager = _authManager;
            dbAccess = _dbAccess;
        }

        // POST api/<LoginController>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Post([FromQuery] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            //No Password Hashing and Salting has been implemented so far
            //So we just check the username and password directly

            //check for record existance non case sensitive
            var user = dbAccess.GetSystemUsers().Where(a => a.Email == loginRequest.Email && a.PasswordHash == loginRequest.Password).FirstOrDefault();
            if(user == null)
                return Ok(new { Response = "Incorrect Credentials" });

            var email = user.Email;
            var userPwd = user.PasswordHash;
            //Check for case sensitivity on email and password
            if (!(email.Equals(loginRequest.Email) && userPwd.Equals(loginRequest.Password)))
                return Ok(new { Response = "Incorrect Credentials" });

            
            ClaimsIdentity claims = new ClaimsIdentity();
            if (user.RoleId == 1)
            {
                claims = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, "Admin"),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, "Admin"),
                }, "ApplicationCookie");

            }
            else
            {
                claims = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, "User"),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, "User"),
            }, "ApplicationCookie");
              
            }
            var claimsPrincipal = new ClaimsPrincipal(claims);

            var authResult = authManager.GenerateTokens(loginRequest.Email, claimsPrincipal.Claims.ToArray(), DateTime.Now);

            return Ok(new LoginResult
            {
                UserId = loginRequest.Email,
                AccessToken = authResult.AccessToken,
                RefreshToken = authResult.RefreshToken
            });
        }

        
    }

  

}
