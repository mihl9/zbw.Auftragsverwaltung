using AutoMapper;
using System;
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

        public async Task<PaginatedList<ArticleGroupDto>> GetCTERecursiveArticleGroup(int size = 10, int page = 1)
        {
            var cteArticleGroups = await _articleGroupRepository.GetCTERecursive(size,page);
            return _mapper.Map<PaginatedList<ArticleGroupDto>>(cteArticleGroups);

        }

        
    }
}
