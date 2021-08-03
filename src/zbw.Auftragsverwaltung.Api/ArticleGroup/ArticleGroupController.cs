﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using zbw.Auftragsverwaltung.Api.Common.Models;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Dto;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Interfaces;
using zbw.Auftragsverwaltung.Core.Common.DTO;
using zbw.Auftragsverwaltung.Core.Common.Exceptions;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace zbw.Auftragsverwaltung.Api.ArticleGroup
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleGroupController : ControllerBase
    {
        private readonly IArticleGroupBll _articleGroupBll;
        private readonly ILogger<ArticleGroupController> _logger;
        private readonly UserManager<User> _userManager;


        public ArticleGroupController(IArticleGroupBll articleGroupBll, ILogger<ArticleGroupController> logger)
        {
            _articleGroupBll = articleGroupBll;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<ArticleGroupDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetList(int size = 10, int page = 1, bool deleted = false)
        {

            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            try
            {
                var result = await _articleGroupBll.GetList(deleted, size, page);
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

        // GET api/<ArticleGroupController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PaginatedList<ArticleGroupDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            try
            {
                var result = await _articleGroupBll.Get(id);
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


        [HttpPost]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        [ProducesResponseType(typeof(PaginatedList<ArticleGroupDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add([FromBody] ArticleGroupDto article)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            try
            {
                var result = await _articleGroupBll.Add(article);
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

        // PUT api/<ArticleGroupController>/5
        [HttpPatch]
        [ProducesResponseType(typeof(ArticleGroupDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] ArticleGroupDto articleGroup)
        {

            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            try
            {
                var result = await _articleGroupBll.Update(articleGroup);
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

        // DELETE api/<ArticleGroupController>/5
        [HttpDelete]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dto = new ArticleGroupDto() { Id = id };

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            try
            {
                var result = await _articleGroupBll.Delete(dto);
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
