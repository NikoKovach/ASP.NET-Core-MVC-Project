using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Services.ServiceContracts;
using Payroll.Services.UtilitiesServices.EntityValidateServices;
using Payroll.ViewModels;
using Payroll.ViewModels.EmpContractViewModels;

namespace PersonnelWebApp.Areas.LaborAgreements.Controllers
{
       public class AgreementTypesController : Controller
       {
              private IAgreementTypeService service;
              private IValidate<ValidateBaseModel> validateService;

              public AgreementTypesController( IAgreementTypeService service,
                      [FromKeyedServices( "StringValueExists" )] IValidate<ValidateBaseModel> validateService )
              {
                     this.service = service;

                     this.validateService = validateService;
              }

              [HttpGet]
              public async Task<IActionResult> GetTypes()
              {
                     return await ResultAsync();
              }

              [HttpPost]
              public async Task<IActionResult> CreateType( [FromBody] AgreementTypeVM? contractType )
              {
                     object[] validateServiceDictionaryParams =
                             { nameof( AgreementTypeVM ), nameof( contractType.Type ), contractType.Type };

                     this.validateService.Validate( ModelState, contractType, "", validateServiceDictionaryParams );

                     if ( !ModelState.IsValid )
                     {
                            return Json( ModelState );
                     }

                     await this.service.AddAsync( contractType );

                     return await ResultAsync();
              }

              [HttpPost]
              public async Task<IActionResult> EditType( [FromBody] AgreementTypeVM? contractType )
              {
                     object[] validateServiceDictionaryParams =
                           { nameof( AgreementTypeVM ), nameof( contractType.Type ), contractType.Type };

                     this.validateService.Validate( ModelState, contractType, "", validateServiceDictionaryParams );

                     if ( !ModelState.IsValid )
                     {
                            return Json( ModelState );
                     }

                     await this.service.UpdateAsync( contractType );

                     return await ResultAsync();
              }

              [HttpGet]
              public async Task<IActionResult> GetAgreementType( int? id )
              {
                     if ( id == null || id < 1 )
                     {
                            return Json( string.Empty );
                     }

                     AgreementTypeVM? agreementTypeVM = await this.service.GetAgreementType( id )
                                                                                                                                 .FirstOrDefaultAsync();

                     return Json( agreementTypeVM );
              }

              //######################################################

              private async Task<IActionResult> ResultAsync()
              {
                     List<AgreementTypeVM>? typesResult = await this.service
                                                                                                                .AllAgreements().ToListAsync();

                     return Json( typesResult );
              }

       }
}
