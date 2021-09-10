using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using zbw.Auftragsverwaltung.Core.Addresses.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Domain.Addresses;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Customers;

namespace zbw.Auftragsverwaltung.Api.Address
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBll _addressBll;
        private readonly ILogger<AddressController> _logger;
        private readonly UserManager<User> _userManager;

        public AddressController(IAddressBll articleGroupBll, ILoggerFactory loggerFactory, UserManager<User> userManager)
        {
            _addressBll = articleGroupBll;
            _logger = loggerFactory.CreateLogger<AddressController>();
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AddressDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _addressBll.Get(id, userId);
            return Ok(result);

        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<AddressDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetList(int size = 10, int page = 1, bool deleted = false)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _addressBll.GetList(userId, deleted, size, page);
            return Ok(result);

        }

        [HttpPost]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        [ProducesResponseType(typeof(AddressDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add([FromBody] AddressDto address)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _addressBll.Add(address, userId);
            return Ok(result);

        }

        [HttpPatch]
        [ProducesResponseType(typeof(AddressDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] AddressDto address)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _addressBll.Update(address, userId);
            return Ok(result);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dto = new AddressDto() { Id = id };

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _addressBll.Delete(dto, userId);
            return Ok();

        }
    }
}
