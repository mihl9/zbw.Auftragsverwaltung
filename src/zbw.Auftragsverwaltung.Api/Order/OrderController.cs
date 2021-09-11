using System;
using System.Threading.Tasks;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using zbw.Auftragsverwaltung.Api.Common.Models;
using zbw.Auftragsverwaltung.Core.Orders.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Orders;

namespace zbw.Auftragsverwaltung.Api
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;
        private readonly IOrderBll _orderBll;
        private readonly UserManager<User> _userManager;

        public OrderController(ILogger<OrderController> logger, IOrderBll orderBll, UserManager<User> userManager)
        {
            _logger = logger;
            _orderBll = orderBll;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            return new JsonResult(await _orderBll.Get(id));
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetList(int size = 10, int page = 1, bool deleted = false)
        {
            return new JsonResult(await _orderBll.GetList(deleted, size, page));
        }

        [HttpPost]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        public async Task<OrderDto> Add([FromBody] OrderDto order)
        {
            if (!User.IsInRole(Roles.Administrator.ToString()))
            {
                var ord = await _orderBll.Get(order.Id);

            }

            return await _orderBll.Add(order);
        }

        [HttpPatch]
        [ProducesResponseType(typeof(OrderDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] OrderDto order)
        {
            return new JsonResult(await _orderBll.Update(order));
        }

        [HttpDelete]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var dto = new OrderDto() { Id = id };

            var result = await _orderBll.Delete(dto);
            if (result)
                return Ok(new SuccessMessage());

            return new BadRequestObjectResult(new ErrorMessage() { Message = $"Failed to delete the Order with the ID: {id}" });
        }
    }
}
