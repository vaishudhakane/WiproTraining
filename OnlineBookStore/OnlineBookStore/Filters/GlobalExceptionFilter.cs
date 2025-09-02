using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace OnlineBookStore.Filters;

public class GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger) : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception, "Unhandled exception");
        context.Result = new ViewResult
        {
            ViewName = "/Pages/Error" // falls back to default error page if not present
        };
        context.ExceptionHandled = true;
    }
}
