using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PersonnelWebApp.Filters
{
       public class ExceptionFilter : IExceptionFilter
       {
              private readonly IWebHostEnvironment currentEnvironment;

              public ExceptionFilter( IWebHostEnvironment environment )
              {
                     this.currentEnvironment = environment;
              }

              public void OnException( ExceptionContext context )
              {
                     if ( this.currentEnvironment.IsDevelopment() )
                            return;

                     ViewResult? viewResult = new ViewResult()
                     {
                            ViewName = "Error",
                     };

                     context.Result = viewResult;
              }
       }
}
