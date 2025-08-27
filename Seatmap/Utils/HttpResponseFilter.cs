using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq.Expressions;
using System.Reflection;

namespace Seatmap.Utils
{
    public class HttpResponseFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null) 
            {
                context.Result = new ObjectResult(new Response(context.Exception.Message));
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
    }
}
