namespace Core.Utilities.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message, IReadOnlyList<string>? errors = null)
            : base(message, "NOT_FOUND", 404, errors ?? new List<string>()) { }
        public NotFoundException(string message)
            : base(message, "NOT_FOUND", 404) { }
    }
}
