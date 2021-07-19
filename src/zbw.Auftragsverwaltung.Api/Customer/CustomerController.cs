using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Extensions;
using zbw.Auftragsverwaltung.Api.Common.Models;
using zbw.Auftragsverwaltung.Core.Common.DTO;
using zbw.Auftragsverwaltung.Core.Customers.Dto;
using zbw.Auftragsverwaltung.Core.Customers.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;

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

        [HttpGet]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            if (!User.IsInRole(Roles.Administrator.ToString()))
            {
                var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!Guid.TryParse(rawUserId, out var userId))
                {
                    return Forbid();
                }

                var result = await _customerBll.Get(id);

                if (!result.UserId.Equals(userId.ToString()))
                {
                    return Forbid();
                }

                return new JsonResult(result);
            }
            
            return new JsonResult(await _customerBll.Get(id));
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<CustomerDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetList(int size = 10, int page = 1, bool deleted = false)
        {
            if (!User.IsInRole(Roles.Administrator.ToString()))
            {
                var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!Guid.TryParse(rawUserId, out var userId))
                {
                    return Forbid();
                }

                return new JsonResult(await _customerBll.GetList(x => x.UserId.Equals(userId), size, page));
            }

            return new JsonResult(await _customerBll.GetList(deleted, size, page));
        }

        [HttpPost]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        public async Task<CustomerDto> Add([FromBody] CustomerDto customer)
        {
            if (!User.IsInRole(Roles.Administrator.ToString()))
            {
                var cust = await _customerBll.Get(customer.Id);
                                                
            }
            
            return await _customerBll.Add(customer);
        }

        [HttpPatch]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] CustomerDto customer)
        {
            if (!User.IsInRole(Roles.Administrator.ToString()))
            {
                var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!Guid.TryParse(rawUserId, out var userId))
                {
                    return Forbid();
                }

                var result = await _customerBll.Get(customer.Id);

                if (!result.UserId.Equals(userId.ToString()))
                {
                    return Forbid();
                }

                return new JsonResult(await _customerBll.Update(customer));
            }
            return new JsonResult(await _customerBll.Update(customer));
        }

        [HttpDelete]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var dto = new CustomerDto(){Id = id};

            var result = await _customerBll.Delete(dto);
            if (result)
                return Ok(new SuccessMessage());

            return new BadRequestObjectResult(new ErrorMessage(){Message = $"Failed to delete the Customer with the ID: {id}"});
        }
    }
}
