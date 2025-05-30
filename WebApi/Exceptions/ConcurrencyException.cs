namespace WebApi.Exceptions
{
    public class ConcurrencyException : BaseException
    {
        public ConcurrencyException(string message, IReadOnlyList<string>? errors = null)
            : base(message, "CONCURRENCY_ERROR", StatusCodes.Status409Conflict, errors ?? new List<string>()) { }

    }
}
