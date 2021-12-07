namespace Core.Utils.Result
{
    public class ErrorDataResult<T> : ErrorResult, IDataResult<T>
    {
        public ErrorDataResult(T data, string message) : base(message)
        {
            Data = data;
        }

        public ErrorDataResult(T data) : base()
        {
            Data = data;
        }

        public ErrorDataResult(string message) : base(message)
        {
            
        }
        
        public T Data { get; }
    }
}