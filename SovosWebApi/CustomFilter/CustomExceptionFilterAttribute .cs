using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SovosWebApi.CustomFilter
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //if (context.Exception is HttpResponseException exception)
            //{
            //    context.Result = new JsonResult(new { error = exception.Message }) { StatusCode = 400 };
            //    context.ExceptionHandled = true;
            //}
        }
        
    }
}
