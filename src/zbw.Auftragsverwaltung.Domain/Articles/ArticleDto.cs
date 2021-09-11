using System;
using zbw.Auftragsverwaltung.Domain.ArticleGroups;

namespace zbw.Auftragsverwaltung.Domain.Articles
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
