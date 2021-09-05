using System;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Domain.ArticleGroups;

namespace zbw.Auftragsverwaltung.Core.ArticleGroups.Interfaces
{
    public interface IArticleGroupBll : ICrudBll<ArticleGroupDto,ArticleGroup,Guid>
    {

    }
}
