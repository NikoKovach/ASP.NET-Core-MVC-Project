using Microsoft.AspNetCore.Mvc;
using Payroll.Services.Services.CompanyServices;
using Payroll.ViewModels;

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
			ICollection<CompanyViewModel> companyList = 
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
          public async Task<IActionResult> Create(CompanyViewModel modelDto)
          {
               if ( !ModelState.IsValid)
               {
				return View(nameof(Create));                  
               }
             
			try
               {				
				await this.service.AddAsync(modelDto);

				string appFolderPath = Path.Combine( this.env.ContentRootPath,
							   this.config[ "PrimaryAppFolder:FolderName" ] );


				this.service.CreateUpdateCompanyFolder(appFolderPath,modelDto,
					nameof(Create));

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

               CompanyViewModel company = await 
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
		public async Task<IActionResult> EditCompany(CompanyViewModel modelDto,
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
							   this.config[ "PrimaryAppFolder:FolderName" ] );

				this.service.CreateUpdateCompanyFolder( appFolderPath, 
					modelDto,nameof(EditCompany), oldName );

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

               CompanyViewModel company = await 
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
