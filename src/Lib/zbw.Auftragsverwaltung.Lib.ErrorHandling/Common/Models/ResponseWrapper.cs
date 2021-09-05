namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Models
{
    public class ResponseWrapper<TResponse> where TResponse : class
    {
        public ResponseWrapper(TResponse response)
        {
            Response = response;
        }

        public TResponse Response { get; }
    }
}
