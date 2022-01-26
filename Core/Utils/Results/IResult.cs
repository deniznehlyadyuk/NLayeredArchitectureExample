namespace Core.Utils.Results
{
    public interface IResult
    {
        public bool Success { get; set; }
        public string Description { get; set; }
    }
}