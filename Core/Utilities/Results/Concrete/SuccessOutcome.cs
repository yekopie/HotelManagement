namespace Core.Utilities.Results.Concrete
{
    public class SuccessOutcome : Outcome
    {
        public SuccessOutcome(string message = "") : base(true, message)
        {
        }
    }
}
