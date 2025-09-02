using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace OnlineBookStore.Filters;

public class LoggingActionFilter(ILogger<LoggingActionFilter> logger) : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
        => logger.LogInformation("Executing {Action} with route {RouteValues}",
            context.ActionDescriptor.DisplayName, context.RouteData.Values);

    public void OnActionExecuted(ActionExecutedContext context)
        => logger.LogInformation("Executed {Action}", context.ActionDescriptor.DisplayName);
}
