namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Helpers
{
    public class ErrorHandlerDefaults
    {
        public const string ExceptionType = "x-exception-type";
        public const string IsDomain = "x-is-domain";
        public const string ExceptionCode = "x-exception-code";
        public const string TraceIdentifier = "x-trace-identifier";
        public const string Message = "ex-message";
        public const string StackTrace = "ex-stack-trace";
        public const string Source = "ex-source";
        public const string Method = "ex-method";
        public const string InnerException = "ex-inner";

        public const char ExceptionCodeSeparator = '|';
    }
}
