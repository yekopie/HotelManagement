namespace Core.Utilities.Results.Concrete
{
    public class ErrorDataOutcome<TData> : DataOutcome<TData>
    {
        public ErrorDataOutcome(TData data, string message = "") : base(data, false, message)
        {
        }
    }
}
