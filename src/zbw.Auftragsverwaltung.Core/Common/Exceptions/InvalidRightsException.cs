using System;
using System.Collections.Generic;
using System.Text;

namespace zbw.Auftragsverwaltung.Core.Common.Exceptions
{
    public class InvalidRightsException : Exception
    {
        public InvalidRightsException()
        {
        }

        public InvalidRightsException(string message) : base(message)
        {
        }

        public InvalidRightsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
