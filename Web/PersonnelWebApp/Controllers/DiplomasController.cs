using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Payroll.Services.Services;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PersonViewModels;

namespace PersonnelWebApp.Controllers
{
       public class DiplomasController : Controller
       {
              private const int PageSize = 6;
              private const int PageIndex = 1;
              private const int Count = 1;

              private readonly IDiplomaService service;

              public DiplomasController( IDiplomaService diplomaService )
              {
                     this.service = diplomaService;
              }

              [HttpPost]
              public async Task<IActionResult> Index(
                     [Required(ErrorMessage ="Select person from person's table ! Go back to 'Persons List .'")]
                     int? personId,
                     int? pageIndex, int? pageSize, string? sortParam )
              {
                     if ( !ModelState.IsValid )
                     {
                            return await ResultAsync( personId, pageIndex, pageSize, sortParam );
                     }

                     //var actionEdit = nameof(Edit);
                     return await ResultAsync( personId, pageIndex, pageSize, sortParam );
              }

              [HttpPost]
              public async Task<IActionResult> Create( int? personId, int? pageIndex, int? pageSize,
                     string? sortParam, DiplomaVM? diplomaVM )
              {
                     if ( !ModelState.IsValid )
                     {
                            return await ResultAsync( personId, pageIndex, pageSize, sortParam );
                     }

                     await this.service.AddAsync( diplomaVM );

                     return await ResultAsync( personId, pageIndex, pageSize, sortParam );
              }


              [HttpPost]
              public async Task<IActionResult> Edit( [Required] int? personId, int? pageIndex, int? pageSize, string? sortParam,
                                                                                 List<DiplomaVM>? entitiesForEdit )
              {
                     if ( !ModelState.IsValid )
                     {
                            if ( entitiesForEdit.Count > 0 )
                            {
                                   return Json( ModelState );
                            }

                            return await ResultAsync( personId, pageIndex, pageSize, sortParam );
                     }

                     await this.service.UpdateAsync( entitiesForEdit );

                     if ( entitiesForEdit.Count > 0 )
                     {
                            return Json( ModelState );
                     }

                     return await ResultAsync( personId, pageIndex, pageSize, sortParam );
              }

              [HttpPost]
              public async Task<IActionResult> Delete(
                     [Required] int? personId,
                     [Required] int? diplomaId,
                     int? pageIndex, int? pageSize, string? sortParam )
              {
                     if ( !ModelState.IsValid )
                     {
                            return await ResultAsync( personId, pageIndex, pageSize, sortParam );
                     }

                     await this.service.DeleteAsync( diplomaId, personId );

                     return await ResultAsync( personId, pageIndex, pageSize, sortParam );
              }

              //##############################################################

              private async Task<IActionResult> ResultAsync( int? personId, int? pageIndex, int? pageSize, string? sortParam )
              {
                     PagingListSorted<DiplomaVM>? pagingSortedList =
                             await GetDiplomasListOfPagesAsync( personId, pageIndex, pageSize, sortParam );

                     string? controllerName = this.RouteData.Values[ "controller" ].ToString();

                     pagingSortedList.RouteEdit = $"/{controllerName}/{nameof( Edit )}";

                     return View( "Index", pagingSortedList );
              }

              private PagingListSorted<DiplomaVM> EmptyPersonsSortedCollection()
              {
                     List<DiplomaVM> defaultDiplomasList = new List<DiplomaVM>()
                     {
                            new DiplomaVM()
                     };

                     PagingListSorted<DiplomaVM> emptyPaginatedList = new PagingListSorted<DiplomaVM>
                            ( defaultDiplomasList, Count, PageIndex, PageSize, string.Empty );

                     return emptyPaginatedList;
              }

              private async Task<PagingListSorted<DiplomaVM>>? GetDiplomasListOfPagesAsync
                     ( int? personId, int? pageIndex, int? pageSize, string? sortParam )
              {
                     IQueryable<DiplomaVM>? sortedDiplomasList = this.service.AllNotDeleted( personId, sortParam );

                     PagingListSorted<DiplomaVM>? sortedList =
                            await PagingListSorted<DiplomaVM>.CreateSortedCollectionAsync
                               ( sortedDiplomasList, pageIndex ?? PageIndex, pageSize ?? PageSize, sortParam );

                     if ( sortedList.ItemsCollection.Count == 0 )
                            return EmptyPersonsSortedCollection();

                     return sortedList;
              }
       }
}
