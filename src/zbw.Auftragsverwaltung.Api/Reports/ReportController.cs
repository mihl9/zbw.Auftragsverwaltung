using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.Reports.Interfaces;
using zbw.Auftragsverwaltung.Domain.ArticleGroups;
using zbw.Auftragsverwaltung.Domain.Common;

namespace zbw.Auftragsverwaltung.Api.Reports
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportBll reportBll;

        public ReportController(IReportBll reportBll)
        {
            this.reportBll = reportBll;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<ArticleGroupDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCteRecursiveData()
        {

            var result = await reportBll.GetCTERecursiveArticleGroup();
            return Ok(result);

        }
    }
}
