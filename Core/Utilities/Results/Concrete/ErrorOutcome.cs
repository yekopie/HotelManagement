namespace Core.Utilities.Results.Concrete
{
    public class ErrorOutcome : Outcome
    {
        public ErrorOutcome(bool isSuccess, string message) : base(false, string.Empty)
        {
        }
    }
}
