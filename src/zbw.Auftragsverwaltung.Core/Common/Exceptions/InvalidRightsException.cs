using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Enumerations;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Exceptions;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Models;

namespace zbw.Auftragsverwaltung.Core.Common.Exceptions
{
    public class InvalidRightsException : Exception
    {
        public InvalidRightsException(User user) : base("Invalid Rights")
        {
        }
    }
}
