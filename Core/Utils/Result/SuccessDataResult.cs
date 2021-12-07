namespace Core.Utils.Result
{
    public class SuccessDataResult<T> : SuccessResult, IDataResult<T>
    {
        public SuccessDataResult(T data) : base()
        {
            Data = data;
        }

        public T Data { get; }
    }
}