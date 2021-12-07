namespace Core.Utils.Result
{
    public class SuccessResult : IResult
    {
        public SuccessResult(string message)
        {
            Success = true;
            Message = message;
        }

        public SuccessResult()
        {
            Success = true;
        }
        
        public bool Success { get; }
        public string Message { get; }
    }
}