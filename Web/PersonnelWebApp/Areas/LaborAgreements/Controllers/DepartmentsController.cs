using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Services.ServiceContracts;
using Payroll.Services.UtilitiesServices.EntityValidateServices;
using Payroll.ViewModels;
using Payroll.ViewModels.EmpContractViewModels;

namespace PersonnelWebApp.Areas.LaborAgreements.Controllers
{
       public class DepartmentsController : Controller
       {
              private IDepartmentService service;
              private IValidate<ValidateBaseModel> validateService;

              public DepartmentsController( IDepartmentService service,
                     [FromKeyedServices( "StringValueExists" )] IValidate<ValidateBaseModel> validateService )
              {
                     this.service = service;

                     this.validateService = validateService;
              }

              [HttpGet]
              public async Task<IActionResult> Get()
              {
                     return await ResultAsync();
              }

              [HttpPost]
              public async Task<IActionResult> Create( [FromBody] DepartmentVM? departmentVM )
              {
                     //  verification that Department Name not exist in DB
                     object[] checkDictionaryParams =
                            { nameof( DepartmentVM ), nameof( departmentVM.Name ), departmentVM.Name };

                     this.validateService.Validate( ModelState, departmentVM, "", checkDictionaryParams );

                     if ( !ModelState.IsValid )
                     {
                            return Json( ModelState );
                     }

                     await this.service.AddAsync( departmentVM );

                     return await ResultAsync();
              }

              [HttpPost]
              public async Task<IActionResult> Edit( [FromBody] DepartmentVM? departmentVM )
              {
                     object[] checkDictionaryParams =
                           { nameof( DepartmentVM ), nameof( departmentVM.Name ), departmentVM.Name };

                     this.validateService.Validate( ModelState, departmentVM, "", checkDictionaryParams );

                     if ( !ModelState.IsValid )
                     {
                            return Json( ModelState );
                     }

                     await this.service.UpdateAsync( departmentVM );

                     return await ResultAsync();
              }

              [HttpGet]
              public async Task<IActionResult> GetDepartment( int? id )
              {
                     if ( id == null || id < 1 )
                     {
                            return Json( string.Empty );
                     }

                     DepartmentVM? departmentVM = await this.service.GetEntity( id ).FirstOrDefaultAsync();

                     return Json( departmentVM );
              }

              //######################################################

              private async Task<IActionResult> ResultAsync()
              {
                     List<DepartmentVM>? articlesResult = await this.service.All().ToListAsync();

                     return Json( articlesResult );
              }

       }
}
