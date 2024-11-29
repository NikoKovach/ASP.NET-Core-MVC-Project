using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.EmpContractViewModels;

namespace PersonnelWebApp.Areas.LaborAgreements.Controllers
{
       public class AgreementTypesController : Controller
       {
              private readonly IAgreementTypeService service;

              public AgreementTypesController( IAgreementTypeService service )
              {
                     this.service = service;
              }

              [HttpGet]
              public async Task<IActionResult> GetTypes()
              {
                     return await ResultAsync();
              }

              [HttpPost]
              public async Task<IActionResult> CreateType( [FromBody] AgreementTypeVM? contractType )
              {
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
                     //  verification that contractType.Type not exist in DB
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
