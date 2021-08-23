using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Models;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Enumerations;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Exceptions;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Models
{
    public class DomainExceptionDetails : ExceptionProblemDetails
    {
        public DomainExceptionDetails(DomainException ex)
            : base(ex, ex?.Details?.Status ?? StatusCodes.Status500InternalServerError, ex?.Details)
        {
            // override potential sensitive data according to registered base error descriptor
            if (ex?.Details != null && ex.Details is DomainProblemDetails domainDetail && domainDetail.ErrorDescriptor.HasLinkedErrors())
            {
                var baseError = domainDetail.ErrorDescriptor.GetBaseError();

                Type = baseError.Name;
                Title = null;
                Detail = null;
                Instance = null;
                Extensions.Clear();
            }
        }

    }
}
