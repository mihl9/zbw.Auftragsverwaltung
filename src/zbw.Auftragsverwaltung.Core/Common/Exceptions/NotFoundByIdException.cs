using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Enumerations;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Exceptions;

namespace zbw.Auftragsverwaltung.Core.Common.Exceptions
{
    public class NotFoundByIdException : Exception
    {
        public NotFoundByIdException(string message) : base(message)
        {
        }
    }
}
