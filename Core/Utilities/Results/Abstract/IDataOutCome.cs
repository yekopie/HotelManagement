namespace Core.Utilities.Results.Abstract
{
    public interface IDataOutcome<TData> : IOutcome
    {
        TData Data { get; }
    }
}
