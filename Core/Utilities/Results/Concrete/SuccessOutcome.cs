namespace Core.Utilities.Results.Concrete
{
    public class SuccessOutcome : Outcome
    {
        public SuccessOutcome(bool isSuccess, string message) : base(true, string.Empty)
        {
        }
    }
}
