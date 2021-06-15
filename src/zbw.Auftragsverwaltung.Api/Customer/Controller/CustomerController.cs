using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using zbw.Auftragsverwaltung.Core.Customers.BLL;
using zbw.Auftragsverwaltung.Core.Customers.Dto;

namespace zbw.Auftragsverwaltung.Api.Customer.Controller
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly ILogger<CustomerController> _logger;
        private readonly CustomerBll _customerBll;

        public CustomerController(ILogger<CustomerController> logger, CustomerBll customerBll)
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
