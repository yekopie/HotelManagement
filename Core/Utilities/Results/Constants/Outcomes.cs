

using Core.Utilities.Results.Concrete;

namespace Core.Utilities.Results.Constants
{
    public static class Outcomes
    {
        public static readonly SuccessOutcome Success= new SuccessOutcome();
        public static readonly ErrorOutcome Error= new ErrorOutcome();
    }
}
