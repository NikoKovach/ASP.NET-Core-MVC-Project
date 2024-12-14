using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PersonnelWebApp.Filters
{
       public class ExceptionFilter : IExceptionFilter
       {
              public void OnException( ExceptionContext context )
              {
                     var viewResult = new ViewResult()
                     {
                            ViewName = "Error",
                     };

                     context.Result = viewResult;
              }
       }
}
