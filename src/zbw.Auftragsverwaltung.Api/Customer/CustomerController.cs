using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using zbw.Auftragsverwaltung.Api.Common.Models;
using zbw.Auftragsverwaltung.Core.Common.DTO;
using zbw.Auftragsverwaltung.Core.Customers.Dto;
using zbw.Auftragsverwaltung.Core.Customers.Interfaces;

namespace zbw.Auftragsverwaltung.Api.Customer
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerBll _customerBll;

        public CustomerController(ILogger<CustomerController> logger, ICustomerBll customerBll)
        {
            _logger = logger;
            _customerBll = customerBll;
        }

        [HttpGet("{id}")]
        public async Task<CustomerDto> Get(Guid id)
        {
            return await _customerBll.Get(id);
        }

        [HttpGet]
        public async Task<PaginatedList<CustomerDto>> GetList(int size = 10, int page = 1, bool deleted = false)
        {
            return await _customerBll.GetList(deleted, size, page);
        }

        [HttpPost]
        public async Task<CustomerDto> Add([FromBody] CustomerDto customer)
        {
            return await _customerBll.Add(customer);
        }

        [HttpPatch]
        public async Task<CustomerDto> Update([FromBody] CustomerDto customer)
        {
            return await _customerBll.Update(customer);
        }

        [HttpDelete]
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
