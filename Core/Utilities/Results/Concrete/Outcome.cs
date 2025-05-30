using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Results.Concrete
{
    public class Outcome : IOutcome
    {
        public string Message { get; }

        public bool IsSuccess { get; }
        public Outcome(bool isSuccess, string message = "")
        {
            Message = message;
            IsSuccess = isSuccess;
        }

    }
}
