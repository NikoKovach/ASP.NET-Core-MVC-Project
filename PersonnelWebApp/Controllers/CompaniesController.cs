using Microsoft.AspNetCore.Mvc;
using Payroll.Models;
using Payroll.ModelsDto;
using Payroll.Services.Services.CompanyServices;
using Payroll.Services.Services.ServiceContracts;

namespace PersonnelWebApp.Controllers
{
     public class CompaniesController : Controller
     {
          private ICompany getService;
          private IAddUpdateEntity addUpdateService;

          public CompaniesController(ICompany companyService,
               IAddUpdateEntity modifiedService)
          {
               getService = companyService;
               addUpdateService = modifiedService;
          }

          public async Task<IActionResult> AllActual()
          {
               return View(await getService.GetAllValidEntitiesAsync());
          }

          public async Task<IActionResult> Details(string uniqueIdentifier)
          {
               CompanyDto company = await getService.GetActiveCompanyByUniqueIdAsync( uniqueIdentifier );

               return View(company);
          }

          public IActionResult Create()
          { 
               
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Create(CompanyDto modelDto)
          {
               if ( ModelState.IsValid)
               {
                    try
                    {
                         await addUpdateService
                               .AddEntityAsync<Company,CompanyDto>(modelDto);

                         return RedirectToAction(nameof(AllActual));
                    }
                    catch ( Exception ex )
                    {

                         return View("Error");
                    }
                    
               }

               return View(nameof(Create));
          }

          public IActionResult Update()
          { 
               return View();
          }
          public async Task<IActionResult> About()
          { 
               return View();
          }
     }
}
