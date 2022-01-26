namespace Core.Utils.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult() : base(true)
        {
        }

        public SuccessResult(string description) : base(true, description)
        {
        }
    }
}