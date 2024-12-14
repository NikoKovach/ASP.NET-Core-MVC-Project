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
                     ICollection<CompanyVM> companyList =
                            await this.service.AllActive().ToListAsync();

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
              public async Task<IActionResult> Create( CompanyVM modelDto )
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
                     //if ( string.IsNullOrWhiteSpace( uniqueIdentifier ) )
                     if ( !ModelState.IsValid )
                     {
                            return RedirectToAction( "Error", "Home" );
                     }

                     CompanyVM? company =
                            await this.service.AllActive( uniqueIdentifier ).FirstOrDefaultAsync();

                     if ( company == null )
                     {
                            return RedirectToAction( "Error", "Home" );
                     }

                     return View( company );
              }

              [HttpPost]
              public async Task<IActionResult> EditCompany( CompanyVM modelDto, string oldName )
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
                     //if ( string.IsNullOrWhiteSpace( uniqueIdentifier ) )
                     if ( !ModelState.IsValid )
                     {
                            return RedirectToAction( "Error", "Home" );
                     }

                     CompanyVM? company =
                            await this.service.AllActive( uniqueIdentifier ).FirstOrDefaultAsync();

                     return View( company );
              }

              public IActionResult About_Us()
              {
                     return View( "About" );
              }

              [HttpGet]
              public async Task<IActionResult> GetCompany( int? id )
              {
                     if ( id == null || id < 1 )
                     {
                            return Json( string.Empty );
                     }

                     string? companyName = await this.service
                                                                                  .GetEntity( id )
                                                                                  .FirstOrDefaultAsync();

                     return Json( companyName );
              }
       }
}

