using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PagingViewModels;
using Payroll.ViewModels.PersonViewModels;
using PersonnelWebApp.Utilities;

namespace PersonnelWebApp.Controllers
{
       public class IdDocumentsController : Controller
       {
              private int _pageSize;
              private int _pageIndex;
              private int _count;

              private readonly IDocumentsService service;
              private IConfigurationRoot? privateConfig;

              public IdDocumentsController( IDocumentsService documentsService, IPrivateConfiguration configuration )
              {
                     this.service = documentsService;

                     this.privateConfig = configuration.PrivateConfig();

                     SetPagingVariables();
              }

              public async Task<IActionResult> AllDocuments(
                      [Required(ErrorMessage ="Select person from person's table ! Go back to 'Persons List' .")]
                      int? personId,
                      int? pageIndex, int? pageSize, string? sortParam )
              {
                     if ( !ModelState.IsValid )
                     {
                            return await ResultAsync( personId, pageIndex, pageSize, sortParam );
                     }

                     return await ResultAsync( personId, pageIndex, pageSize, sortParam );
              }

              [HttpPost]
              public async Task<IActionResult> Create(
                     [Required(ErrorMessage ="Select person from person's table ! Go back to 'Persons List' .")]
                      int? personId,
                      int? pageIndex, int? pageSize, string? sortParam, IdDocumentVM documentVM )
              {
                     if ( !ModelState.IsValid )
                     {
                            return await ResultAsync( personId, pageIndex, pageSize, sortParam );
                     }

                     await this.service.AddAsync( documentVM );

                     return await ResultAsync( personId, pageIndex, pageSize, sortParam );
              }

              [HttpPost]
              public async Task<IActionResult> Edit(
                     [Required(ErrorMessage ="Select person from person's table ! Go back to 'Persons List' .")]
                      int? personId,
                      int? pageIndex, int? pageSize, string? sortParam, List<IdDocumentVM>? entitiesForEdit )
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
                     [Required] int? documentId,
                     int? pageIndex, int? pageSize, string? sortParam )
              {
                     if ( !ModelState.IsValid )
                     {
                            return await ResultAsync( personId, pageIndex, pageSize, sortParam );
                     }

                     await this.service.DeleteAsync( documentId, personId );

                     return await ResultAsync( personId, pageIndex, pageSize, sortParam );
              }

              //********************************************************************

              private async Task<IActionResult> ResultAsync( int? personId, int? pageIndex, int? pageSize, string? sortParam )
              {
                     PagingListSorted<IdDocumentVM>? pagingSortedList =
                             await GetIdDocumentsListOfPagesAsync( personId, pageIndex, pageSize, sortParam );

                     string? controllerName = this.RouteData.Values[ "controller" ].ToString();

                     pagingSortedList.RouteEdit = $"/{controllerName}/{nameof( Edit )}";

                     return View( "AllDocuments", pagingSortedList );
              }

              private async Task<PagingListSorted<IdDocumentVM>>? GetIdDocumentsListOfPagesAsync
                    ( int? personId, int? pageIndex, int? pageSize, string? sortParam )
              {
                     IQueryable<IdDocumentVM>? sortedDocumentsList = this.service.AllNotDeleted( personId, sortParam );

                     if ( sortedDocumentsList is null )
                            return EmptyDocumentsSortedCollection( personId );

                     PagingListSorted<IdDocumentVM>? sortedList =
                            await PagingListSorted<IdDocumentVM>.CreateSortedCollectionAsync
                               ( sortedDocumentsList, pageIndex ?? this._pageIndex, pageSize ?? this._pageSize, sortParam );

                     if ( sortedList.ItemsCollection.Count == 0 )
                            return EmptyDocumentsSortedCollection( personId );

                     return sortedList;
              }

              private PagingListSorted<IdDocumentVM> EmptyDocumentsSortedCollection( int? personId )
              {
                     List<IdDocumentVM> defaultDocumentsList = new List<IdDocumentVM>()
                     {
                            new IdDocumentVM{PersonId = personId,}
                     };

                     PagingListSorted<IdDocumentVM> emptyPaginatedList = new PagingListSorted<IdDocumentVM>
                            ( defaultDocumentsList, this._count, this._pageIndex, this._pageSize, string.Empty );

                     return emptyPaginatedList;
              }

              private void SetPagingVariables()
              {
                     if ( !string.IsNullOrEmpty( this.privateConfig[ "Paging:PageSize" ] ) )
                     {
                            this._pageSize = int.Parse( this.privateConfig[ "Paging:PageSize" ] );
                     }

                     if ( !string.IsNullOrEmpty( this.privateConfig[ "Paging:PageIndex" ] ) )
                     {
                            this._pageIndex = int.Parse( this.privateConfig[ "Paging:PageIndex" ] );
                     }

                     if ( !string.IsNullOrEmpty( this.privateConfig[ "Paging:Count" ] ) )
                     {
                            this._count = int.Parse( this.privateConfig[ "Paging:Count" ] );
                     }
              }
       }
}
