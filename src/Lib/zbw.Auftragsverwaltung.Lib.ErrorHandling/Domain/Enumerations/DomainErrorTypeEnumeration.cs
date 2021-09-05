using System;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Enumerations;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Models;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Enumerations
{
    public class DomainErrorTypeEnumeration : ErrorTypeEnumeration
    {
        public DomainErrorTypeEnumeration(int id, Uri name, DomainErrorTypeEnumeration genericNonSensitiveType = null)
            : base(new ErrorBaseType(id, name?.AbsoluteUri, genericNonSensitiveType?.ErrorType))
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
        }

        public static Uri BaseUri { get; } = new Uri("http://localhost/");

        public static DomainErrorTypeEnumeration InternalServerError =>
            new DomainErrorTypeEnumeration(50000, new Uri(BaseUri, "internal-server-error"));

        public static DomainErrorTypeEnumeration EntityNotFoundById =>
            new DomainErrorTypeEnumeration(50100, new Uri(BaseUri, "entity-not-found-by-id"));

        public static DomainErrorTypeEnumeration CustomServerError =>
            new DomainErrorTypeEnumeration(9999, new Uri(BaseUri, "custom-server-error"));
    }
}
