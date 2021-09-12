using System;
using System.Threading.Tasks;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using zbw.Auftragsverwaltung.Api.Common.Models;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Core.Positions.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Domain.Positions;

namespace zbw.Auftragsverwaltung.Api.Position
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PositionController : ControllerBase
    {

        private readonly ILogger<PositionController> _logger;
        private readonly IPositionBll _positionBll;
        private readonly UserManager<User> _userManager;

        public PositionController(ILogger<PositionController> logger, IPositionBll positionBll, UserManager<User> userManager)
        {
            _logger = logger;
            _positionBll = positionBll;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PositionDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _positionBll.Get(id);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<PositionDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetList(int size = 10, int page = 1, bool deleted = false)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _positionBll.GetList( deleted, size, page);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        public async Task<IActionResult> Add([FromBody] PositionDto order)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _positionBll.Add(order);
            return Ok(result);
        }

        [HttpPatch]
        [ProducesResponseType(typeof(PositionDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] PositionDto order)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _positionBll.Update(order);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dto = new PositionDto() { Id = id };

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _positionBll.Delete(dto);
            return Ok();
        }
    }
}
