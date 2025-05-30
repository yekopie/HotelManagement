using Microsoft.AspNetCore.Mvc;

namespace WebApi.Middlewares
{
    public class CustomProblemDetails : ProblemDetails
    {
        public string Title { get; set; }
        public int StatusCode { get; set; }
        public string Detail { get; set; }
        public string ErrorCode { get; set; }
        public List<string> Errors { get; set; }
    }
}
