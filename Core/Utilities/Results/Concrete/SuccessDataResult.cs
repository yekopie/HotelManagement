namespace Core.Utilities.Results.Concrete
{
    public class SuccessDataOutcome<TData> : DataOutcome<TData>
    {
        public SuccessDataOutcome(TData data, string message = "") : base(data, true, message)
        {
        }
    }
}
