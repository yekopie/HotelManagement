namespace Core.Utilities.Results.Concrete
{
    public class ErrorOutcome : Outcome
    {
        public ErrorOutcome(string message = "") : base(false, message)
        {
        }
    }
}
