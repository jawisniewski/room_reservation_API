using Microsoft.AspNetCore.Mvc;
using RoomBooking.Application.Common;

namespace RoomBooking.API.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                return new ObjectResult(result.Value) { StatusCode = (int)result.StatusCode };

            var problem = new ProblemDetails
            {
                Title = result.StatusCode.ToString(),
                Detail = result.Message,
            };

            return new ObjectResult(problem) { StatusCode = (int)result.StatusCode };
        }
        public static IActionResult ToActionResult(this Result result)
        {
            if (result.IsSuccess)
                return new ObjectResult(true) { StatusCode = (int) result.StatusCode };

            var problem = new ProblemDetails
            {
                Title = result.StatusCode.ToString(),
                Detail = result.Message,
            };

            return new ObjectResult(problem) { StatusCode = (int)result.StatusCode };
        }
    }
}
