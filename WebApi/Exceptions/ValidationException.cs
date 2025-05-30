
namespace WebApi.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(string message, IReadOnlyList<string>? errors = null) 
            : base(message, "VALIDATION_ERROR", StatusCodes.Status400BadRequest, errors)
        {
        }
    }
}
