using System;
using System.Collections.Generic;
using System.Text;

namespace zbw.Auftragsverwaltung.Core.Common.Interfaces
{
    public interface IEntityHistorized : IEntity
    {
        DateTime ValidFrom { get; set; }
        
        DateTime? ValidTo { get; set; }

    }
}
