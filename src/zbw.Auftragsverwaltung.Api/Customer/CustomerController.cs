using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using zbw.Auftragsverwaltung.Api.Common.Models;
using zbw.Auftragsverwaltung.Core.Common.Exceptions;
using zbw.Auftragsverwaltung.Core.Customers.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Customers;

namespace zbw.Auftragsverwaltung.Api.Customer
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerBll _customerBll;
        private readonly UserManager<User> _userManager;

        public CustomerController(ILogger<CustomerController> logger, ICustomerBll customerBll, UserManager<User> userManager)
        {
            _logger = logger;
            _customerBll = customerBll;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _customerBll.Get(id, userId);
            return Ok(result);

        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<CustomerDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetList(int size = 10, int page = 1, bool deleted = false)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _customerBll.GetList(userId, deleted, size, page);
            return Ok(result);

        }

        [HttpPost]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        [ProducesResponseType(typeof(PaginatedList<CustomerDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add([FromBody] CustomerDto customer)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _customerBll.Add(customer, userId);
            return Ok(result);

        }

        [HttpPatch]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] CustomerDto customer)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _customerBll.Update(customer, userId);
            return Ok(result);
            
        }

        [HttpDelete]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dto = new CustomerDto() { Id = id };

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _customerBll.Delete(dto, userId);
            return Ok();

        }
    }
}
