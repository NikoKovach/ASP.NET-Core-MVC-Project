using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Services.ServiceContracts;
using Payroll.Services.UtilitiesServices.EntityValidateServices;
using Payroll.ViewModels;
using Payroll.ViewModels.EmployeeViewModels;
using Payroll.ViewModels.PagingViewModels;

namespace PersonnelWebApp.Controllers
{
       public class EmployeesController : Controller
       {
              private const int PageSize = 1;
              private const int PageIndex = 1;
              private const int Count = 1;

              private readonly IEmployeeService empService;
              private readonly IValidate<ValidateBaseModel> validateService;
              private readonly IWebHostEnvironment env;
              private readonly IConfiguration config;

              private static int companyIdNumber = 0;

              public EmployeesController( IEmployeeService service,
                     [FromKeyedServices( "EmployeeValidate" )] IValidate<ValidateBaseModel> validateService,
                      IWebHostEnvironment environment, IConfiguration configuration )
              {
                     this.empService = service;

                     this.validateService = validateService;

                     this.env = environment;

                     this.config = configuration;
              }

              public IActionResult Index()
              {
                     var emptyPaginatedList = new PagingBaseList<GetEmployeeVM>
                            ( EmptyEmpoyeesCollection(), Count, PageIndex, PageSize );

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

                     var paginatedList = await PagingBaseList<GetEmployeeVM>
                                                 .CreateCollectionAsync( empList, pageNumber ?? 1, PageSize );

                     if ( paginatedList.ItemsCollection.Count == 0 )
                     {
                            var emptyPaginatedList = new PagingBaseList<GetEmployeeVM>
                                   ( EmptyEmpoyeesCollection(), Count, PageIndex, PageSize );

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
                                   IsPresent = true,
                            };

                            return View( empModel );
                     }

                     EmployeeVM? empViewModel = await this.empService
                                                                                                  .GetEntity( id )
                                                                                                  .FirstOrDefaultAsync();

                     return View( empViewModel );
              }

              [HttpPost]
              public async Task<IActionResult> CreateEmployee( EmployeeVM empViewModel )
              {
                     this.validateService.Validate( ModelState, empViewModel );

                     if ( !ModelState.IsValid )
                     {
                            return View( nameof( Create ) );
                     }

                     empViewModel.IsPresent = true;

                     await this.empService.AddAsync( empViewModel );

                     string appFolderPath = Path.Combine( this.env.ContentRootPath,
                                             this.config[ "PrimaryAppFolder:FolderName" ] );

                     bool empFolderWasCreate = await this.empService.CreateEmployeeFolderAsync( appFolderPath,
                            empViewModel.PersonId, empViewModel.CompanyId );

                     if ( empViewModel.ProfileImage != null && empFolderWasCreate == true )
                     {
                            string? empImageFullPath = await this.empService.UploadEmployeePictureAsync
                                                                                                                ( empViewModel, appFolderPath );

                            string? appFolderName = this.config[ "PrimaryAppFolder:FolderName" ];
                            string? relativeFolderName = this.config[ "PrimaryAppFolder:RequestPath" ];

                            await this.empService.UpdatePersonAsync( empViewModel.PersonId, empImageFullPath,
                                                                                                                        relativeFolderName, appFolderName );
                     }

                     List<AllEmployeeVM>? employeeList = await this.empService
                                                                                          .AllActive_AllEmployeeVM( empViewModel.CompanyId )
                                                                                          .ToListAsync();

                     return View( nameof( AllPresent ), employeeList );
              }

              [HttpPost]
              public async Task<IActionResult> EditEmployee( EmployeeVM empViewModel )
              {
                     this.validateService.Validate( ModelState, empViewModel );

                     if ( !ModelState.IsValid )
                            return View( nameof( Create ) );

                     empViewModel.IsPresent = true;

                     await this.empService.UpdateAsync( empViewModel );

                     string appFolderPath = Path.Combine( this.env.ContentRootPath,
                                                                                          this.config[ "PrimaryAppFolder:FolderName" ] );

                     bool empFolderWasCreate = await this.empService.CreateEmployeeFolderAsync( appFolderPath,
                                                                                           empViewModel.PersonId, empViewModel.CompanyId );

                     if ( empViewModel.ProfileImage != null && empFolderWasCreate == true )
                     {
                            string? empImageFullPath = await this.empService.UploadEmployeePictureAsync
                                                                                                                ( empViewModel, appFolderPath );

                            string? appFolderName = this.config[ "PrimaryAppFolder:FolderName" ];
                            string? relativeFolderName = this.config[ "PrimaryAppFolder:RequestPath" ];

                            await this.empService.UpdatePersonAsync( empViewModel.PersonId, empImageFullPath,
                                                                                                                        relativeFolderName, appFolderName );
                     }
                     //********************************************************************
                     List<AllEmployeeVM>? employeeList = await this.empService
                                                                                          .AllActive_AllEmployeeVM( empViewModel.CompanyId )
                                                                                          .ToListAsync();

                     return View( nameof( AllPresent ), employeeList );
              }

              [HttpPost]
              public async Task<IActionResult> Delete( int? employeeId )
              {
                     if ( !ModelState.IsValid )
                     {
                            return View();
                     }

                     EmployeeVM? empViewModel = await this.empService
                                                                                                  .GetEntity( employeeId )
                                                                                                  .FirstOrDefaultAsync();

                     empViewModel.IsPresent = false;

                     await this.empService.UpdateAsync( empViewModel );

                     List<AllEmployeeVM>? employeeList = await this.empService
                                                                                           .AllActive_AllEmployeeVM( empViewModel.CompanyId )
                                                                                           .ToListAsync();

                     return View( nameof( AllPresent ), employeeList );
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

              [HttpGet]
              public async Task<IActionResult> GetEmloyeesByCompany( int? id )
              {
                     List<SearchEmployeeVM>? result = await this.empService
                             .AllActive_SearchEmployeeVM( id )
                             .ToListAsync();

                     return Json( result );
              }

              [HttpGet]
              public async Task<IActionResult> GetEmployee( int? id )
              {
                     if ( id == null || id < 1 )
                     {
                            return Json( string.Empty );
                     }

                     string? employeeFullName = await this.empService.GetEmployeeName( id );

                     return Json( employeeFullName );
              }

              //*******************************************************************

              private List<GetEmployeeVM> EmptyEmpoyeesCollection()
              {
                     GetEmployeeVM defaultItem = new GetEmployeeVM()
                     {
                            Person = new PersonEmpVM(),
                            ContactInfo = new LaborContactVM(),
                            IdCardPassport = new IdDocumentEmpVM(),
                            ContractInfo = new ContractEmpVM(),
                     };

                     var items = new List<GetEmployeeVM>
                     {
                            defaultItem
                     };

                     return items;
              }
       }
}


