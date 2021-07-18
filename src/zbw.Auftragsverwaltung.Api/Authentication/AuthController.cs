using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
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
using zbw.Auftragsverwaltung.Api.Authentication.Models;
using zbw.Auftragsverwaltung.Api.Common.Models;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
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
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public AuthController(IOptions<JwtBearerSettings> jwtBearerSettings, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _jwtBearerSettings = jwtBearerSettings.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(SuccessMessage), (int)HttpStatusCode.OK)]
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

            await _userManager.AddToRoleAsync(user, Roles.User.ToString());
            return Ok(new SuccessMessage() {Message = "Registration Complete"});
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticatedMessage), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(ErrorMessage))]
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
                var token = await GenerateToken(user);

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

        private async Task<object> GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtBearerSettings.Secret);
            var userClaims = await _userManager.GetClaimsAsync(user);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddSeconds(_jwtBearerSettings.ExpiryTimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtBearerSettings.Audience,
                Issuer = _jwtBearerSettings.Issuer
            };
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    tokenDescriptor.Subject.AddClaims(roleClaims);
                }
            }
            tokenDescriptor.Subject.AddClaims(userClaims);
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
        #endregion
    }
}
