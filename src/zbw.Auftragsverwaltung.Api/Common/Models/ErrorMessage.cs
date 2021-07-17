using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zbw.Auftragsverwaltung.Api.Common.Models
{
    public class ErrorMessage : BaseMessage
    {
        public object Errors { get; set; }
    }
}
