using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Exceptions;

namespace WebAPI.Filters
{
    public class ValidationExceptionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ValidationException exception)
            {
                context.Result = new BadRequestObjectResult(exception.Message);
                context.ExceptionHandled = true;
            }
        }
    }
}
