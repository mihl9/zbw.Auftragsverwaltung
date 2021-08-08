using System;
using System.Threading.Tasks;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using zbw.Auftragsverwaltung.Api.Common.Models;
using zbw.Auftragsverwaltung.Core.Common.DTO;
using zbw.Auftragsverwaltung.Core.Positions.Dto;
using zbw.Auftragsverwaltung.Core.Positions.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;

namespace zbw.Auftragsverwaltung.Api
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
            return new JsonResult(await _positionBll.Get(id));
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<PositionDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetList(int size = 10, int page = 1, bool deleted = false)
        {
            return new JsonResult(await _positionBll.GetList(deleted, size, page));
        }

        [HttpPost]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        public async Task<PositionDto> Add([FromBody] PositionDto order)
        {
            if (!User.IsInRole(Roles.Administrator.ToString()))
            {
                var pos = await _positionBll.Get(order.Id);

            }

            return await _positionBll.Add(order);
        }

        [HttpPatch]
        [ProducesResponseType(typeof(PositionDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] PositionDto order)
        {
            return new JsonResult(await _positionBll.Update(order));
        }

        [HttpDelete]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var dto = new PositionDto() { Id = id };

            var result = await _positionBll.Delete(dto);
            if (result)
                return Ok(new SuccessMessage());

            return new BadRequestObjectResult(new ErrorMessage() { Message = $"Failed to delete the Order with the ID: {id}" });
        }
    }
}
