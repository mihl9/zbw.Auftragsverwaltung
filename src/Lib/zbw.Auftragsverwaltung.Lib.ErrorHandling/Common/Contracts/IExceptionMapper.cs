using System;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Models;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Contracts
{
    public interface IExceptionMapper<TResponse> where TResponse : class
    {
        public void AddMappingIfNotExists(string exceptionName, Func<ProblemDetails, Exception> exceptionMapping);

        public Task EnsureSuccess(ResponseWrapper<TResponse> wrapper);
    }
}
