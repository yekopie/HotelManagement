using Core.Utilities.Results.Concrete;

namespace Core.Utilities.Results
{
    public class SuccessOutcome : Outcome
    {
        public SuccessOutcome(string message = "") : base(true, message)
        {
        }
    }
}
