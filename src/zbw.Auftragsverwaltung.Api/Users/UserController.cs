using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using zbw.Auftragsverwaltung.Api.Common.Models;
using zbw.Auftragsverwaltung.Core.Users.Bll;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Domain.Users;

namespace zbw.Auftragsverwaltung.Api.Users
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserBll _userBll;

        public UserController(ILoggerFactory logger, UserBll userBll)
        {
            _logger = logger.CreateLogger<UserController>();
            _userBll = userBll;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id = null)
        {
            if (!User.IsInRole(Roles.Administrator.ToString()) || id == null)
            {
                id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            
            if (Guid.TryParse(id, out var parsedId))
                return BadRequest(new ErrorMessage() { Message = "Invalid Guid"});

            var user = await _userBll.Get(parsedId);

            return Ok(user);
        }

        [HttpPatch]
        public async Task<IActionResult> Update(UserDto user)
        {
            if (!User.IsInRole(Roles.Administrator.ToString()))
            {
                var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (user.Id.ToString().Equals(id)) return Forbid();
            }
            var result = await _userBll.Update(user);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(UserDto user)
        {
            if (!User.IsInRole(Roles.Administrator.ToString()))
            {
                var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (user.Id.ToString().Equals(id)) return Forbid();
            }

            var result = await _userBll.Delete(user);

            if (result)
            {
                return Ok(new SuccessMessage());
            }

            return BadRequest(new ErrorMessage());
        }
    }
}
