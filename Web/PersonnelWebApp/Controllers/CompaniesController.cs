using CommonServices;
using Microsoft.AspNetCore.Mvc;
using Payroll.ModelsDto;
using Payroll.Services.Services.CompanyServices;

namespace PersonnelWebApp.Controllers
{
     public class CompaniesController : Controller
     {
          private ICompany service;
          private IWebHostEnvironment env;
		private IConfiguration config;

          public CompaniesController(ICompany companyService,
			IWebHostEnvironment environment,IConfiguration configuration)
          {
               this.service = companyService;

			this.env = environment;

			this.config = configuration;
          }

          public async Task<IActionResult> AllActual()
          {
			ICollection<CompanyDto> companyList = 
				await this.service.GetAllValidCompaniesAsync();

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
               if ( !ModelState.IsValid)
               {
				return View(nameof(Create));                  
               }
             
			try
               {				
				await this.service.AddAsync(modelDto);

				string appFolderPath = Path.Combine( this.env.ContentRootPath,
							   this.config[ "PrimaryAppFolder" ] );


				this.service.CreateUpdateCompanyFolder(appFolderPath,modelDto,
					nameof(Create));

				//await this.service.CreateCompanyFolderAsync(appFolderPath,modelDto);

				return RedirectToAction(nameof(AllActual));
               }
               catch ( Exception ex )
               {
                    return View("Error",ex.Message);
               }
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

               CompanyDto company = await 
				this.service.GetActiveCompanyByUniqueIdAsync( uniqueIdentifier );

			if ( company == null)
			{
				//TODO : Later N.Kostov's course
				return RedirectToAction( "Error", "Home" );
			}

               return View(company);
          }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditCompany(CompanyDto modelDto,
			string oldName)
          { 
               if ( !ModelState.IsValid)
               {
                    return View(nameof(Edit),modelDto);                
               }

			try
               {
				if ( modelDto.HasBeenDeleted == true )
				{
					modelDto.DeletionDate = DateTime.UtcNow;
				}

				await this.service.UpdateAsync(modelDto);

				string appFolderPath = Path.Combine( this.env.ContentRootPath,
							   this.config[ "PrimaryAppFolder" ] );

				this.service.CreateUpdateCompanyFolder(appFolderPath,modelDto,
					nameof(EditCompany),oldName);

				return RedirectToAction(nameof(AllActual));
               }
               catch ( Exception ex )
               {
				return RedirectToAction("Error","Home",ex.Message);
                    //TODO : Later N.Kostov's course
               }  
          }

		[HttpPost]
		public async Task<IActionResult> Details(string uniqueIdentifier)
          {
			if ( string.IsNullOrWhiteSpace(uniqueIdentifier) )
			{
				//TODO : Later N.Kostov's course
				return RedirectToAction("Error","Home");
			}

               CompanyDto company = await 
				this.service.GetActiveCompanyByUniqueIdAsync( uniqueIdentifier );

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
