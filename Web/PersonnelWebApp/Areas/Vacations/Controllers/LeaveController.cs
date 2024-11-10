using Microsoft.AspNetCore.Mvc;

namespace PersonnelWebApp.Areas.Leave.Controllers
{
       [Area( "Vacations" )]
       public class LeaveController : Controller
       {
              public ActionResult Index()
              {
                     return View();
              }
       }
}
