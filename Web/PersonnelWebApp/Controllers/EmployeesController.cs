using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Services.EmployeeServices;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.EmployeeViewModels;
using PersonnelWebApp.Filters;

namespace PersonnelWebApp.Controllers
{
       public class EmployeesController : Controller
       {
              private readonly IEmployee empService;
              private readonly IValidateEmployeeVModels validateService;
              private readonly IWebHostEnvironment env;
              private readonly IConfiguration config;

              private static int companyIdNumber = 0;

              public EmployeesController( IEmployee service, IValidateEmployeeVModels validateService,
                                                               IWebHostEnvironment environment, IConfiguration configuration )
              {
                     this.empService = service;

                     this.validateService = validateService;

                     this.env = environment;

                     this.config = configuration;
              }

              public IActionResult Index()
              {
                     var emptyPaginatedList = new PaginatedCollection<GetEmployeeVM>
                            ( EmptyEmpoyeesCollection(), 1, 1 );

                     return View( emptyPaginatedList );
              }

              [HttpPost]
              public async Task<IActionResult> Index( int companyId, int? pageNumber )
              {
                     if ( companyId != 0 )
                     {
                            companyIdNumber = companyId;
                     }

                     IQueryable<GetEmployeeVM>? empList = this.empService
                                                                                                       .AllActive_GetEmployeeVM( companyIdNumber );

                     var paginatedList = await PaginatedCollection<GetEmployeeVM>
                                                 .CreateCollectionAsync( empList, pageNumber ?? 1 );

                     if ( paginatedList.ItemsCollection.Count == 0 )
                     {
                            var emptyPaginatedList = new PaginatedCollection<GetEmployeeVM>
                                   ( EmptyEmpoyeesCollection(), 1, 1 );

                            return Json( emptyPaginatedList );
                     }

                     return Json( paginatedList );
              }

              [HttpPost]
              public async Task<IActionResult> Create( int? id )
              {
                     if ( id == null || id < 1 )
                     {
                            EmployeeVM empModel = new EmployeeVM
                            {
                                   PersonId = 0,
                                   CompanyId = 0,
                                   IsPresent = true
                            };

                            return View( empModel );
                     }

                     EmployeeVM? empViewModel = await this.empService.GetEntityAsync( id );

                     return View( empViewModel );
              }

              [HttpPost]
              [FileValidationFilter( [ ".jpg", ".png" ], 1024 * 1024 )]
              public async Task<IActionResult> CreateEmployee( EmployeeVM empViewModel )
              //IFormFile? employeeImage 
              {
                     ValidateModel( empViewModel );

                     if ( !ModelState.IsValid )
                     {
                            return View( nameof( Create ) );
                     }

                     empViewModel.IsPresent = true;
                     await this.empService.AddAsync( empViewModel );

                     string appFolderPath = Path.Combine( this.env.ContentRootPath,
                                             this.config[ "PrimaryAppFolder:FolderName" ] );

                     string? employeeFolder = await this.empService.CreateEmployeeFolderAsync( appFolderPath,
                            empViewModel.PersonId, empViewModel.CompanyId );

                     if ( empViewModel.ProfileImage != null )
                     {
                            await ManageFileAsync( empViewModel.ProfileImage, employeeFolder );
                     }

                     /*
                            TODO
                            2.Запис на адреса на снимката от сървъра в Database - table Person,field - string? PhotoFilePath
                     */
                     //**********************End save image*********************************
                     List<AllEmployeeVM>? employeeList = await this.empService
                                                                                                            .AllActive_AllEmployeeVM( empViewModel.CompanyId )
                                                                                                            .ToListAsync();

                     return View( nameof( AllPresent ), employeeList );
              }

              [HttpPost]
              public async Task<IActionResult> EditEmployee( EmployeeVM empViewModel,
                                                                                                         IFormFile? employeeImage )
              {

                     if ( !ModelState.IsValid )
                     {

                            //return View( nameof( Create ),empViewModel );
                     }

                     await this.empService.UpdateAsync( empViewModel );

                     //Implement empService.AddAsync method
                     //Създаване на папка за служител
                     //Копиране снимка на човек на сървъра.

                     return View();
              }

              [HttpPost]
              public async Task<IActionResult> Delete( int? employeeId )
              {
                     if ( !ModelState.IsValid )
                     {
                            return View();
                     }

                     EmployeeVM? empViewModel = await this.empService.GetEntityAsync( employeeId );
                     empViewModel.IsPresent = false;

                     await this.empService.UpdateAsync( empViewModel );

                     return View();
              }

              [HttpPost]
              public async Task<IActionResult> AllPresent( int companyId )
              {
                     if ( !ModelState.IsValid )
                     {
                            return RedirectToAction( nameof( Index ) );
                     }

                     int firmId = ( companyId > 0 ) ? companyId : 1;

                     List<AllEmployeeVM>? employeeList = await this.empService
                                                                                                            .AllActive_AllEmployeeVM( firmId )
                                                                                                            .ToListAsync();
                     return View( employeeList );
              }
              //*******************************************************************

              private List<GetEmployeeVM> EmptyEmpoyeesCollection()
              {
                     GetEmployeeVM defaultItem = new GetEmployeeVM()
                     {
                            Person = new PersonEmpVM(),
                            ContactInfo = new ContactsEmpVM(),
                            IdCardPassport = new IdDocumentEmpVM(),
                            ContractInfo = new ContractEmpVM(),
                     };

                     var items = new List<GetEmployeeVM>
                     {
                            defaultItem
                     };

                     return items;
              }

              private async Task ManageFileAsync( IFormFile imageFile, string? employeeFolder )
              {
                     /* 1.Копиране снимка на човек на сървъра.
                                   1.1 validate that file is picture
                                   1.2 if file is picture - > save to employee folder
                     */

                     if ( string.IsNullOrEmpty( employeeFolder ) )
                     {
                            return;
                     }

                     string fileNameWithPath = this.empService
                                                                            .CreateFileNameWithPath( employeeFolder, imageFile.FileName );

                     FileStream? stream = new FileStream( fileNameWithPath, FileMode.Create );

                     await imageFile.CopyToAsync( stream );
              }

              private void ValidateModel( EmployeeVM empViewModel )
              {
                     if ( empViewModel.NumberFromTheList != null )
                     {
                            this.validateService.NumberFromTheListIsValid( empViewModel.NumberFromTheList,
                                                                      empViewModel.CompanyId, this.empService.Repository );

                            if ( this.validateService.EntityState.ModelIsValid == false )
                            {
                                   ModelState.AddModelError( nameof( empViewModel.NumberFromTheList ),
                                                                                    this.validateService.EntityState.ErrorMessage );
                            }
                     }

              }
       }
}


/*
      
*/