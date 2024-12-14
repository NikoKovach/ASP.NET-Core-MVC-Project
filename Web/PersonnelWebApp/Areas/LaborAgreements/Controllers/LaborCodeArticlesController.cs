using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Services.ServiceContracts;
using Payroll.Services.UtilitiesServices.EntityValidateServices;
using Payroll.ViewModels;
using Payroll.ViewModels.EmpContractViewModels;

namespace PersonnelWebApp.Areas.LaborAgreements.Controllers
{
       [Area( "LaborAgreements" )]
       public class LaborCodeArticlesController : Controller
       {
              private ILaborCodeArticleService service;
              private IValidate<ValidateBaseModel> validateService;

              public LaborCodeArticlesController( ILaborCodeArticleService service,
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
              public async Task<IActionResult> CreateType( [FromBody] LaborCodeArticleVM? articleTypeVM )
              {
                     object[] validateServiceDictionaryParams =
                             { nameof( LaborCodeArticleVM ), nameof( articleTypeVM.Article ), articleTypeVM.Article };

                     await this.validateService.ValidateAsync( ModelState, articleTypeVM, "", validateServiceDictionaryParams );

                     if ( !ModelState.IsValid )
                     {
                            return Json( ModelState );
                     }

                     await this.service.AddAsync( articleTypeVM );

                     return await ResultAsync();
              }

              [HttpPost]
              public async Task<IActionResult> EditType( [FromBody] LaborCodeArticleVM? articleTypeVM )
              {
                     object[] validateServiceDictionaryParams =
                            { nameof( LaborCodeArticleVM ), nameof( articleTypeVM.Article ), articleTypeVM.Article };

                     await this.validateService.ValidateAsync( ModelState, articleTypeVM, "", validateServiceDictionaryParams );

                     if ( !ModelState.IsValid )
                     {
                            return Json( ModelState );
                     }

                     await this.service.UpdateAsync( articleTypeVM );

                     return await ResultAsync();
              }

              [HttpGet]
              public async Task<IActionResult> GetLaborArticle( int? id )
              {
                     if ( id == null || id < 1 )
                     {
                            return Json( string.Empty );
                     }

                     string? articleName = await this.service.GetEntity( id ).FirstOrDefaultAsync();

                     return Json( articleName );
              }

              //######################################################

              private async Task<IActionResult> ResultAsync()
              {
                     List<LaborCodeArticleVM>? articlesResult = await this.service.AllArticles().ToListAsync();

                     return Json( articlesResult );
              }

       }
}
