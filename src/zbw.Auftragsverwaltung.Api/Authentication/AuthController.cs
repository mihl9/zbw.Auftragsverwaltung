using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using zbw.Auftragsverwaltung.Api.Common.Models;
using zbw.Auftragsverwaltung.Core.Common.Configurations;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Core.Users.Interfaces;
using zbw.Auftragsverwaltung.Domain.Users;

namespace zbw.Auftragsverwaltung.Api.Authentication
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserBll _userBll;

        public AuthController(IUserBll userBll)
        {
            _userBll = userBll;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(SuccessMessage), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest userDetails)
        {
            if(!ModelState.IsValid || userDetails == null)
                return new BadRequestResult();

            if (await _userBll.Register(userDetails))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticateResponse), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ErrorMessage))]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequest credentials)
        {
            if (!ModelState.IsValid || credentials == null)
                return new BadRequestObjectResult(ModelState);

            return Ok(await _userBll.Authenticate(credentials, GetIpAddress()));

        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _userBll.Logout();
            return Ok(new SuccessMessage());
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest token)
        {
            var response = await _userBll.RefreshToken(token.Token, GetIpAddress());

            return Ok(response);
        }
        [HttpPost("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest request)
        {
            var response = await _userBll.RevokeToken(request, GetIpAddress());

            if (response)
            {
                return Ok();
            }

            return BadRequest();
        }

        #region Helpers

        private string GetIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        #endregion
    }
}
