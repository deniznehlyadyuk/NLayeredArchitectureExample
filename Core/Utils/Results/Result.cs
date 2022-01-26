namespace Core.Utils.Results
{
    public abstract class Result : IResult
    {
        public bool Success { get; set; }
        public string Description { get; set; }

        public Result(bool success)
        {
            Success = success;
        }

        public Result(bool success, string description)
        {
            Success = success;
            Description = description;
        }
    }
}