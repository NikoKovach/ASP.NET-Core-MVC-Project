using Microsoft.AspNetCore.Mvc;
using Payroll.ModelsDto;
using Payroll.Services.Services.CompanyServices;

namespace PersonnelWebApp.Controllers
{
     public class CompanyController : Controller
     {
          private ICompany service;

          public CompanyController(ICompany companyService)
          {
               this.service = companyService;
          }

          public IActionResult AllActual()
          {
               return View(service.GetAllValidEntities().ToList());
          }

          public IActionResult Details(string companyId)
          {
               CompanyDto company = this.service.GetActiveCompanyByUniqueId( companyId );

               return View(company);
          }

          public IActionResult Create()
          { 

               return View();
          }

          [HttpPost]
          public IActionResult Create(CompanyDto model)
          { 

               return View();
          }

          public IActionResult About()
          { 

               return View();
          }
     }
}
