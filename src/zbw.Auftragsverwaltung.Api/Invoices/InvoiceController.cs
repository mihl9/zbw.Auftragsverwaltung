using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using zbw.Auftragsverwaltung.Core.Invoices.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Domain.Invoices;
using zbw.Auftragsverwaltung.Domain.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace zbw.Auftragsverwaltung.Api.Invoices
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly ILogger<InvoiceController> _logger;
        private readonly IInvoiceBll _invoiceBll;

        public InvoiceController(ILogger<InvoiceController> logger, IInvoiceBll invoiceBll)
        {
            _logger = logger;
            _invoiceBll = invoiceBll;
        }
        
        // GET api/<InvoiceController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InvoiceDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _invoiceBll.Get(id);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<InvoiceDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetList(int size = 10, int page = 1, bool deleted = false)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _invoiceBll.GetList(deleted, size, page);
            return Ok(result);

        }

        // POST api/<InvoiceController>
        [HttpPost]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        [ProducesResponseType(typeof(InvoiceDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add([FromBody] InvoiceDto invoice)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _invoiceBll.Add(invoice);
            return Ok(result);

        }

        // PUT api/<InvoiceController>/5
        [HttpPatch]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        [ProducesResponseType(typeof(InvoiceDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] InvoiceDto invoice)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _invoiceBll.Update(invoice);
            return Ok(result);
            
        }

        // DELETE api/<InvoiceController>/5
        [HttpDelete]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        [ProducesResponseType(typeof(InvoiceDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dto = new InvoiceDto() { Id = id };

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            var result = await _invoiceBll.Delete(dto);
            return Ok();

        }
    }
}
