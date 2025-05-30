using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Results.Concrete
{
    public class DataOutcome<TData> : Outcome, IDataOutCome<TData>
    {
        public DataOutcome(TData data, bool isSuccess, string message) : base(isSuccess, message)
        {
            Data = data;
        }

        public TData Data { get; }
    }


}
