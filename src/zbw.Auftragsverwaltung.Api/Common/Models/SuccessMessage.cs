using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zbw.Auftragsverwaltung.Api.Common.Models
{
    public class SuccessMessage : BaseMessage
    {
        public override string Status { get; protected set; } = "Success";
    }
}
