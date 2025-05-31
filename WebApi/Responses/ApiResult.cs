using Core.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Responses
{
    public class ApiResult : ObjectResult
    {
        public ApiStatus? StatusCode { get; set; }
        public ApiResult(IOutcome result, ApiStatus? statusCode = null) : base(result)
        {
            StatusCode = statusCode;
        }
    }
}
