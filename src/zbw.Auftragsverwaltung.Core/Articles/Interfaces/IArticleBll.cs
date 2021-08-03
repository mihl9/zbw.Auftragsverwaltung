using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.Articles.Dto;
using zbw.Auftragsverwaltung.Core.Articles.Entities;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;

namespace zbw.Auftragsverwaltung.Core.Articles.Interfaces
{
    public interface IArticleBll : ICrudBll<ArticleDto,Article,Guid>
    {
    }
}
