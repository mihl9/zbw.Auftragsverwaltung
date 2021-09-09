using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Contracts;
using zbw.Auftragsverwaltung.Core.Reports.Interfaces;
using zbw.Auftragsverwaltung.Domain.ArticleGroups;
using zbw.Auftragsverwaltung.Domain.Common;

namespace zbw.Auftragsverwaltung.Core.Reports.BLL
{
    public class ReportBll : IReportBll
    {
        private readonly IArticleGroupRepository _articleGroupRepository;
        private readonly IMapper _mapper;

        public ReportBll(IArticleGroupRepository articleGroupRepository)
        {
            _articleGroupRepository = articleGroupRepository;

        }

        public async Task<IReadOnlyList<ArticleGroupDto>> GetCTERecursiveArticleGroup()
        {
            var cteArticleGroups = await _articleGroupRepository.GetCTERecursive();
            return _mapper.Map<IReadOnlyList<ArticleGroupDto>>(cteArticleGroups);

        }

        
    }
}
