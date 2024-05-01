using Microsoft.AspNetCore.Mvc;

namespace PersonnelWebApp.Controllers
{
     public class CompanyController : Controller
     {
          public IActionResult About()
          {
               return View();
          }

          public IActionResult Details()
          {
               return View();
          }
     }
}
