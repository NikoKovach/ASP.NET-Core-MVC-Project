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
			ICollection<CompanyDto> companyList = 
				await getService.GetAllValidEntitiesAsync();

			if ( companyList == null )
			{
				return NotFound(companyList);
			}

               return View(companyList);
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
                         return View("Error",ex.Message);
                    }
                    
               }

               return View(nameof(Create));
          }

		[HttpPost]
		public async Task<IActionResult> Edit(string uniqueIdentifier)
          { 
			if ( string.IsNullOrWhiteSpace(uniqueIdentifier) )
			{
				//TODO : Later N.Kostov's course
				//return RedirectToAction("Error","Home");

				throw new ArgumentNullException();
			}

               CompanyDto company = await getService.GetActiveCompanyByUniqueIdAsync( uniqueIdentifier );

			if ( company == null)
			{
				//TODO : Later N.Kostov's course
				return RedirectToAction( "Error", "Home" );
			}

               return View(company);
          }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditCompany(CompanyDto modelDto)
          { 
               if ( ModelState.IsValid)
               {
                    try
                    {
					if ( modelDto.HasBeenDeleted == true )
					{
						modelDto.DeletionDate = DateTime.UtcNow;
					}

                         await addUpdateService
                               .UpdateEntityAsync<Company,CompanyDto>(modelDto);

                         return RedirectToAction(nameof(AllActual));
                    }
                    catch ( Exception ex )
                    {
					return RedirectToAction("Error","Home",ex.Message);
                         //TODO : Later N.Kostov's course
                    }                  
               }

               return View(nameof(Edit),modelDto);
          }

		[HttpPost]
		public async Task<IActionResult> Details(string uniqueIdentifier)
          {
			if ( string.IsNullOrWhiteSpace(uniqueIdentifier) )
			{
				//TODO : Later N.Kostov's course
				return RedirectToAction("Error","Home");
			}

               CompanyDto company = await getService.GetActiveCompanyByUniqueIdAsync( uniqueIdentifier );

			if ( company == null)
			{
				//TODO : Later N.Kostov's course
				return RedirectToAction("Error","Home");
			}

               return View(company);
          }

          public IActionResult About_Us()
          {
               return View("About");
          }
     }
}
