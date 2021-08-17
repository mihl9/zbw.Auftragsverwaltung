using System;
using System.Collections.Generic;
using System.Text;

namespace zbw.Auftragsverwaltung.Client.Common.Model
{
    public class RequestContext
    {
        public string Authorization { get; set; }

        public override bool Equals(object obj)
        {
            return obj is RequestContext context &&
                   Authorization == context.Authorization;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Authorization);
        }

    }
}
