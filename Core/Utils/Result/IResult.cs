namespace Core.Utils.Result
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}