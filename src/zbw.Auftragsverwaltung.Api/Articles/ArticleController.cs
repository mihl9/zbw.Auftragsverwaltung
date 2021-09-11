using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using zbw.Auftragsverwaltung.Api.Common.Models;
using zbw.Auftragsverwaltung.Core.Articles.Interfaces;
using zbw.Auftragsverwaltung.Core.Common.Exceptions;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Domain.Articles;
using zbw.Auftragsverwaltung.Domain.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace zbw.Auftragsverwaltung.Api.Articles
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly ILogger<ArticleController> _logger;
        private readonly IArticleBll _articleBll;

        public ArticleController(ILogger<ArticleController> logger, IArticleBll articleBll)
        {
            _logger = logger;
            _articleBll = articleBll;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ArticleDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            try
            {
                var result = await _articleBll.Get(id);
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
        [ProducesResponseType(typeof(PaginatedList<ArticleDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetList(int size = 10, int page = 1, bool deleted = false)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            try
            {
                var result = await _articleBll.GetList(deleted, size, page);
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
        [ProducesResponseType(typeof(PaginatedList<ArticleDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add([FromBody] ArticleDto article)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            try
            {
                var result = await _articleBll.Add(article);
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
        [ProducesResponseType(typeof(ArticleDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] ArticleDto article)
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            try
            {
                var result = await _articleBll.Update(article);
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
            var dto = new ArticleDto() { Id = id };

            if (!Guid.TryParse(rawUserId, out var userId))
            {
                return Forbid();
            }

            try
            {
                var result = await _articleBll.Delete(dto);
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
