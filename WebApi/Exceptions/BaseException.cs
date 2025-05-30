namespace WebApi.Exceptions
{
    public class BaseException : Exception
    {
        public IReadOnlyList<string> Errors { get; }
        public int StatusCode { get; }
        public string ErrorCode { get; }

        protected BaseException(string message, string errorCode, int statusCode, IReadOnlyList<string>? errors = null) : base(message)
        {
            ErrorCode = errorCode ?? "UNDEFINED_ERROR";
            StatusCode = statusCode;
            Errors = errors ?? new List<string>();
        }
    }
}
