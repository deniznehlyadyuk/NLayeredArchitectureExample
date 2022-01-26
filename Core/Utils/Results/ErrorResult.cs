namespace Core.Utils.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult() : base(false)
        {
        }

        public ErrorResult(string description) : base(false, description)
        {
        }
    }
}