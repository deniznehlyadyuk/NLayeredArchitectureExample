namespace Core.Utils.Result
{
    public class ErrorResult : IResult
    {
        public ErrorResult(string message)
        {
            Success = false;
            Message = message;
        }

        public ErrorResult()
        {
            Success = false;
        }
        
        public bool Success { get; }
        public string Message { get; }
    }
}