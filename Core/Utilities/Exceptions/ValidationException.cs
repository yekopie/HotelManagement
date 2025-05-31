namespace Core.Utilities.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(string message, IReadOnlyList<string>? errors = null) 
            : base(message, "VALIDATION_ERROR", 400, errors)
        {
        }

        public ValidationException(string message)
            : base(message, "VALIDATION_ERROR", 400)
        {
        }
    }
}
