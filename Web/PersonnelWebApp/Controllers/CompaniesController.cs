using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels;

namespace PersonnelWebApp.Controllers
{
       public class CompaniesController : Controller
       {
              private ICompanyService service;
              private IWebHostEnvironment env;
              private IConfiguration config;

              public CompaniesController( ICompanyService companyService,
                     IWebHostEnvironment environment, IConfiguration configuration )
              {
                     this.service = companyService;

                     this.env = environment;

                     this.config = configuration;
              }

              public async Task<IActionResult> AllActual()
              {
                     ICollection<CompanyViewModel> companyList = await this.service
                                                                                                                              .AllActive()
                                                                                                                              .ToListAsync();

                     if ( companyList == null )
                     {
                            return NotFound( companyList );
                     }

                     return View( companyList );
              }

              public IActionResult Create()
              {
                     return View();
              }

              [HttpPost]
              [ValidateAntiForgeryToken]
              public async Task<IActionResult> Create( CompanyViewModel modelDto )
              {
                     if ( !ModelState.IsValid )
                     {
                            return View( nameof( Create ) );
                     }

                     await this.service.AddAsync( modelDto );

                     string appFolderPath = Path.Combine( this.env.ContentRootPath,
                                             this.config[ "PrimaryAppFolder:FolderName" ] );

                     this.service.CreateUpdateCompanyFolder( appFolderPath, modelDto,
                            nameof( Create ) );

                     return RedirectToAction( nameof( AllActual ) );
              }

              [HttpPost]
              public async Task<IActionResult> Edit( string? uniqueIdentifier )
              {
                     if ( string.IsNullOrWhiteSpace( uniqueIdentifier ) )
                     {
                            return RedirectToAction( "Error", "Home" );
                     }

                     CompanyViewModel? company = await this.service
                                                                                                    .AllActive( uniqueIdentifier )
                                                                                                    .FirstOrDefaultAsync();

                     if ( company == null )
                     {
                            return RedirectToAction( "Error", "Home" );
                     }

                     return View( company );
              }

              [HttpPost]
              [ValidateAntiForgeryToken]
              public async Task<IActionResult> EditCompany( CompanyViewModel modelDto,
                     string oldName )
              {
                     if ( !ModelState.IsValid )
                     {
                            return View( nameof( Edit ), modelDto );
                     }

                     if ( modelDto.HasBeenDeleted == true )
                     {
                            modelDto.DeletionDate = DateTime.UtcNow;
                     }

                     await this.service.UpdateAsync( modelDto );

                     string appFolderPath = Path.Combine( this.env.ContentRootPath,
                                             this.config[ "PrimaryAppFolder:FolderName" ] );

                     this.service.CreateUpdateCompanyFolder( appFolderPath,
                            modelDto, nameof( EditCompany ), oldName );

                     return RedirectToAction( nameof( AllActual ) );
              }

              [HttpPost]
              public async Task<IActionResult> Details( string? uniqueIdentifier )
              {
                     if ( string.IsNullOrWhiteSpace( uniqueIdentifier ) )
                     {
                            return RedirectToAction( "Error", "Home" );
                     }

                     CompanyViewModel? company = await this.service
                                                                                                    .AllActive( uniqueIdentifier )
                                                                                                    .FirstOrDefaultAsync();

                     return View( company );
              }

              public IActionResult About_Us()
              {
                     return View( "About" );
              }
       }
}

//public IActionResult Error()
//{
//       throw new Exception();
//}