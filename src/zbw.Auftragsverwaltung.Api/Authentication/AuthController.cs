using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using zbw.Auftragsverwaltung.Api.Authentication.Models;
using zbw.Auftragsverwaltung.Api.Common.Models;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Infrastructure.Common.Configurations;

namespace zbw.Auftragsverwaltung.Api.Authentication
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtBearerSettings _jwtBearerSettings;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(IOptions<JwtBearerSettings> jwtBearerSettings, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _jwtBearerSettings = jwtBearerSettings.Value;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDetails userDetails)
        {
            if(!ModelState.IsValid || userDetails == null)
                return new BadRequestResult();

            var user = new User(){UserName = userDetails.UserName, Email =  userDetails.Email};
            var result = await _userManager.CreateAsync(user, userDetails.Password);
            if (!result.Succeeded)
            {
                var states = new ModelStateDictionary();
                foreach (var identityError in result.Errors)
                {
                    states.AddModelError(identityError.Code, identityError.Description);
                }

                return new BadRequestObjectResult(new ErrorMessage() { Message = "Registration Failed", Errors = states });
            }

            return Ok(new BaseMessage() {Message = "Success"});
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Credentials credentials)
        {
            if (!ModelState.IsValid || credentials == null)
                return new BadRequestResult();

            var user = await _userManager.FindByNameAsync(credentials.Username);

            if (user == null)
                return new BadRequestObjectResult(new ErrorMessage() { });


            var signinResult = await _signInManager.PasswordSignInAsync(user, credentials.Password, false, true);
            
            if (signinResult.Succeeded)
            {
                var token = GenerateToken(user);

                return Ok(new AuthenticatedMessage() { Token = token });
            }
            if(signinResult.IsNotAllowed)
            {
                if (!user.EmailConfirmed)
                {
                    return new BadRequestObjectResult(new ErrorMessage() { Message = "Email is not Confirmed" });
                }
                return new BadRequestObjectResult(new ErrorMessage(){ Message = signinResult.ToString() });
            }
            
            if(signinResult.RequiresTwoFactor)
                return new BadRequestObjectResult(new ErrorMessage() { Message = "Requires Two Factor" });
            
            return new BadRequestObjectResult(new ErrorMessage() { Message = signinResult.ToString() });
            
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new SuccessMessage());
        }

        #region Helpers

        private object GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtBearerSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddSeconds(_jwtBearerSettings.ExpiryTimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtBearerSettings.Audience,
                Issuer = _jwtBearerSettings.Issuer
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
        #endregion
    }
}
