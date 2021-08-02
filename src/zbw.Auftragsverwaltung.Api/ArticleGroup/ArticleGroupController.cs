using Microsoft.AspNetCore.Mvc;
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


        public ArticleGroupController(IArticleGroupBll articleGroupBll, ILogger<ArticleGroupController> logger, UserManager<User> userManager)
        {
            _articleGroupBll = articleGroupBll;
            _logger = logger;
            _userManager = userManager;
        }

        // GET: api/<ArticleGroupController>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<ArticleGroupDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetList(int size = 10, int page = 1, bool deleted = false)
        {

            return new JsonResult(await _articleGroupBll.GetList(deleted, size, page));
        }

        // GET api/<ArticleGroupController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PaginatedList<ArticleGroupDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            return new JsonResult(await _articleGroupBll.Get(id));
        }

        // POST api/<ArticleGroupController>
        [HttpPost]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        public async Task<ArticleGroupDto> Add([FromBody] ArticleGroupDto articleGroup)
        {
            if (!User.IsInRole(Roles.Administrator.ToString()))
            {
                var cust = await _articleGroupBll.Get(articleGroup.Id);
                                                
            }
            
            return await _articleGroupBll.Add(articleGroup);
        }

        // PUT api/<ArticleGroupController>/5
        [HttpPatch]
        [ProducesResponseType(typeof(ArticleGroupDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] ArticleGroupDto articleGroup)
        {

            return new JsonResult(await _articleGroupBll.Update(articleGroup));
        }

        // DELETE api/<ArticleGroupController>/5
        [HttpDelete]
        [Authorize(Policy = Policies.RequireAdministratorRole)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var dto = new ArticleGroupDto(){Id = id};

            var result = await _articleGroupBll.Delete(dto);
            if (result)
                return Ok(new SuccessMessage());

            return new BadRequestObjectResult(new ErrorMessage(){Message = $"Failed to delete the ArticleGroup with the ID: {id}"});
        }
    }
}
