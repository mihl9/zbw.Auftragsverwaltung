using System;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Models;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Enumerations
{
    public class ErrorTypeEnumeration : IComparable
    {
        public ErrorTypeEnumeration(ErrorBaseType errorType)
        {
            ErrorType = errorType ?? throw new ArgumentNullException(nameof(errorType));
        }

        public ErrorBaseType ErrorType { get; private set; }

        public override string ToString()
        {
            return ErrorType.Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ErrorTypeEnumeration otherValue))
                return false;

            var typeMatches = GetType() == otherValue.GetType();
            var valueMatches = ErrorType.Id.Equals(otherValue.ErrorType.Id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object other)
        {
            if (other == null)
                return 1;
            else
            {
                return ErrorType.Id.CompareTo(((ErrorTypeEnumeration) other).ErrorType.Id);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
