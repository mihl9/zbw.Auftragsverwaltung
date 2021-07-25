using System;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Dto;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;

namespace zbw.Auftragsverwaltung.Core.ArticleGroups.Interfaces
{
    public interface IArticleGroupBll : ICrudBll<ArticleGroupDto,ArticleGroup,Guid>
    {

    }
}
