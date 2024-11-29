using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.EmpContractViewModels;

namespace PersonnelWebApp.Areas.LaborAgreements.Controllers
{
       public class LaborCodeArticlesController : Controller
       {
              private readonly ILaborCodeArticleService service;

              public LaborCodeArticlesController( ILaborCodeArticleService service )
              {
                     this.service = service;
              }

              [HttpGet]
              public async Task<IActionResult> GetTypes()
              {
                     return await ResultAsync();
              }

              [HttpPost]
              public async Task<IActionResult> CreateType( [FromBody] LaborCodeArticleVM? articleTypeVM )
              {
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
                     //  verification that contractType.Type not exist in DB
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

                     LaborCodeArticleVM? articleVM = await this.service.GetEntity( id ).FirstOrDefaultAsync();

                     return Json( articleVM );
              }

              //######################################################

              private async Task<IActionResult> ResultAsync()
              {
                     List<LaborCodeArticleVM>? articlesResult = await this.service.AllArticles().ToListAsync();

                     return Json( articlesResult );
              }

       }
}
