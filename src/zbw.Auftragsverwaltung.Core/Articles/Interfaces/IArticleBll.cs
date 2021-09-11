using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.Articles.Entities;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Domain.Articles;

namespace zbw.Auftragsverwaltung.Core.Articles.Interfaces
{
    public interface IArticleBll : ICrudBll<ArticleDto,Article,Guid>
    {
    }
}
