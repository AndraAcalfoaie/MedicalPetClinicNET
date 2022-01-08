using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Exceptions;

namespace WebAPI.Filters
{
    public class UnauthorizedExceptionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is UnauthorizedException)
            {
                context.Result = new UnauthorizedResult();
                context.ExceptionHandled = true;
            }
        }
    }
}
