namespace Core.Utils.Results
{
    public class SuccessDataResult<T> : SuccessResult, IDataResult<T>
    {
        public T Data { get; set; }

        public SuccessDataResult(T data) : base()
        {
            Data = data;
        }

        public SuccessDataResult(T data, string description) : base(description)
        {
            Data = data;
        }
    }
}