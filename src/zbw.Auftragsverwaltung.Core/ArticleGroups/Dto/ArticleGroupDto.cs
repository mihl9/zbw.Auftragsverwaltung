using System;
using System.Collections.Generic;
using System.Text;

namespace zbw.Auftragsverwaltung.Core.ArticleGroups.Dto
{
    public class ArticleGroupDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ArticleGroupDto Articlegroupdto { get; set; }

        public string UserId { get; set; }
    }
}
