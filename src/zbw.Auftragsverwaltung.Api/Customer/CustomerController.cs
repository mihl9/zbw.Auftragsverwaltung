using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        [HttpGet]
        public async Task<CustomerDto> Get(Guid id)
        {
            return await _customerBll.Get(id);
        }
    }
}
