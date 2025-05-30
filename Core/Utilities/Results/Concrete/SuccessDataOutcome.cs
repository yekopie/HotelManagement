namespace Core.Utilities.Results.Concrete
{
    public class SuccessDataOutcome<TData> : DataOutcome<TData>
    {
        public SuccessDataOutcome(TData data, bool isSuccess, string message) : base(data, true, string.Empty)
        {
        }
    }


}
