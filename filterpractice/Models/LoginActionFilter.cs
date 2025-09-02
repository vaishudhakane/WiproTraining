using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace filterpractice.Models
{
    public class LoginActionFilter:IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
           Debug.WriteLine($"[Log] Executing action: {context.ActionDescriptor.DisplayName}");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
              Debug.WriteLine($"[Log] Executed action: {context.ActionDescriptor.DisplayName}");

        }
    }
  
}
