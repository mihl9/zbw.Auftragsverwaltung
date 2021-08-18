using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Models;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Configuration
{
    public class ApiExceptionMiddlewareOptions<TContext>
    {
        public ApiExceptionMiddlewareOptions()
        {
            Mappers = new Dictionary<Type, int>();
        }

        public Func<IApplicationBuilder, string> RequestPathFilter { get; set; }

        public Func<TContext, bool> IncludeExceptionDetails { get; set; }

        public Func<TContext, bool> IncludeExceptionName { get; set; }

        public Func<TContext, bool> IncludeExceptionCode { get; set; }

        public Func<TContext, bool> IncludeTraceIdentifier { get; set; }

        public Func<TContext, bool> OverwriteExistingExtensions { get; set; }

        private IDictionary<Type, int> Mappers { get; }

        public void Map<TException>(int statusCode)
            where TException : Exception
        {
            var exType = typeof(TException);
            if (Mappers.ContainsKey(exType))
            {
                Mappers[exType] = statusCode;
            }
            else
            {
                Mappers.Add(exType, statusCode);
            }
        }

        internal bool TryMapProblemDetails(Exception exception, out ProblemDetails problem)
        {
            problem = default;

            var exType = exception.GetType();
            if (Mappers.ContainsKey(exType))
            {
                problem = new ExceptionProblemDetails(exception, Mappers[exType]);

                return true;
            }

            problem = default;
            return false;
        }

    }
}
