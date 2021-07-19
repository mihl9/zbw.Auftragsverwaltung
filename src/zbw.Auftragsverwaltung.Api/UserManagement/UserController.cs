using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using zbw.Auftragsverwaltung.Api.Common.Models;
using zbw.Auftragsverwaltung.Core.Users.Bll;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;

namespace zbw.Auftragsverwaltung.Api.UserManagement
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserBll _userBll;

        public UserController(LoggerFactory logger, UserBll userBll)
        {
            _logger = logger.CreateLogger<UserController>();
            _userBll = userBll;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id = null)
        {
            if (!User.IsInRole(Roles.Administrator.ToString()))
            {
                id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            if (Guid.TryParse(id, out var parsedId))
                return BadRequest(new ErrorMessage() { Message = "Invalid Guid"});

            var user = await _userBll.Get(parsedId));

            return new JsonResult(user);

        }
    }
}
