using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Domain.ArticleGroups;

namespace zbw.Auftragsverwaltung.Core.Articles.Dto
{
    public class ArticleDto
    {
        public Guid Id { get; set; }

        public string ArticleId { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public ArticleGroupDto ArticleGroupDto { get; set; }
    }
}
