using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using zbw.Auftragsverwaltung.Core.Common.Configurations;
using zbw.Auftragsverwaltung.Core.Users.Contracts;
using zbw.Auftragsverwaltung.Core.Users.Entities;

namespace zbw.Auftragsverwaltung.Infrastructure.Users.Services
{
    public class DefaultTokenService : ITokenService
    {
        private readonly JwtBearerSettings _jwtBearerSettings;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public DefaultTokenService(IOptions<JwtBearerSettings> jwtBearerSettings, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtBearerSettings = jwtBearerSettings.Value;
        }

        public async Task<object> GenerateAuthenticationToken(User user)
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
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
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

        public async Task<bool> ValidateAuthenticationToken(object token)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtBearerSettings.Secret);

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtBearerSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtBearerSettings.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            handler.ValidateToken(token.ToString(), tokenValidationParameters, out _);
            return await Task.FromResult(true);
           
        }

        public async Task<RefreshToken> GenerateRefreshToken(string ipAddress)
        {
            using var cryptoService = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            cryptoService.GetBytes(randomBytes);

            return await Task.FromResult(new RefreshToken()
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            });
        }
    }
}
