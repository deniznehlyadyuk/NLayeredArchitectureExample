namespace Core.Utils.Result
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}