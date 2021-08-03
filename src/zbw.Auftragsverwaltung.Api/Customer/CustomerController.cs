﻿using System;
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
using zbw.Auftragsverwaltung.Core.Common.Exceptions;
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
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            try
            {
                var result = await _customerBll.Get(id, userId);
                return Ok(result);
            }
            catch (InvalidRightsException e)
            {
                return Forbid();
            }
            catch (UserNotFoundException e)
            {
                return Forbid();
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorMessage() { Message = e.Message });
            }
            
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

            try
            {
                var result = await _customerBll.GetList(userId, deleted, size, page);
                return Ok(result);
            }
            catch (InvalidRightsException)
            {
                return Forbid();
            }
            catch (UserNotFoundException)
            {
                return Forbid();
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorMessage() { Message = e.Message });
            }

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

            try
            {
                var result = await _customerBll.Add(customer, userId);
                return Ok(result);
            }
            catch (InvalidRightsException)
            {
                return Forbid();
            }
            catch (UserNotFoundException)
            {
                return Forbid();
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorMessage() { Message = e.Message });
            }
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

            try
            {
                var result = await _customerBll.Update(customer, userId);
                return Ok(result);
            }
            catch (InvalidRightsException)
            {
                return Forbid();
            }
            catch (UserNotFoundException)
            {
                return Forbid();
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorMessage() { Message = e.Message });
            }
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

            try
            {
                var result = await _customerBll.Delete(dto, userId);
                return Ok();
            }
            catch (InvalidRightsException)
            {
                return Forbid();
            }
            catch (UserNotFoundException)
            {
                return Forbid();
            }
            catch (Exception e)
            {
                return BadRequest(new ErrorMessage() { Message = e.Message });
            }
        }
    }
}
