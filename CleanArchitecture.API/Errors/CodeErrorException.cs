namespace CleanArchitecture.API.Errors
{
    public class CodeErrorException : CodeErrorResponse
    {
        public string? Details { get; set; } 

        public CodeErrorException(int statuscode, string? message = null, string? details = null) : base(statuscode, message)
        {
            Details = details;
        }
    }
}
