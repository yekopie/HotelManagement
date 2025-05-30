namespace Core.Utilities.Results.Abstract
{
    public interface IDataOutCome<TData> : IOutcome
    {
        TData Data { get; }
    }
}
