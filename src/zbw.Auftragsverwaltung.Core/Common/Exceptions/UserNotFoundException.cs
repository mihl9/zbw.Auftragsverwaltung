using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Enumerations;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Exceptions;

namespace zbw.Auftragsverwaltung.Core.Common.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : this(string.Empty)
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

       
    }
}
