namespace Core.Utilities.Results.Concrete
{
    public class ErrorDataOutcome<TData> : DataOutcome<TData>
    {
        public ErrorDataOutcome(TData data, bool isSuccess, string message) : base(data, false, string.Empty)
        {
        }
    }


}
