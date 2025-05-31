namespace Core.Utilities.Exceptions
{
    public class ConcurrencyException : BaseException
    {
        public ConcurrencyException(string message, IReadOnlyList<string>? errors = null)
            : base(message, "CONCURRENCY_ERROR", 409, errors ?? new List<string>()) { }
        public ConcurrencyException(string message)
            : base(message, "CONCURRENCY_ERROR", 409) { }

    }
}
