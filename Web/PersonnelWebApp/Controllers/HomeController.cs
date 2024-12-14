using Microsoft.AspNetCore.Mvc;

namespace PersonnelWebApp.Controllers
{
       public class HomeController : Controller
       {

              public HomeController()
              {

              }

              public IActionResult Index()
              {
                     return View();
              }


              [ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
              public IActionResult Error()
              {
                     return View( "Error" );
              }

              public IActionResult StatusCodeError( int errorCode )
              {
                     if ( errorCode == 404 )
                     {
                            return View( "Error" );
                     }

                     return this.View();
              }
       }
}