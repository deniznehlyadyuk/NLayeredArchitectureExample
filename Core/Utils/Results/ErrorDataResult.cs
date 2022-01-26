namespace Core.Utils.Results
{
    public class ErrorDataResult<T> : ErrorResult, IDataResult<T>
    {
        public T Data { get; set; }

        public ErrorDataResult() : base()
        {
            
        }

        public ErrorDataResult(string description) : base(description)
        {
        }
    }
}