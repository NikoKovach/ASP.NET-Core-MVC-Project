using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PagingViewModels;
using Payroll.ViewModels.PersonViewModels;
using PersonnelWebApp.Utilities;

namespace PersonnelWebApp.Controllers
{
       public class ContactsController : Controller
       {
              private int _pageSize;
              private int _pageIndex;
              private int _count;

              private readonly IContactInfoService service;
              private IConfigurationRoot? privateConfig;

              public ContactsController( IContactInfoService diplomaService, IPrivateConfiguration configuration )
              {
                     this.service = diplomaService;

                     this.privateConfig = configuration.PrivateConfig();

                     SetPagingVariables();
              }

              [HttpPost]
              public async Task<IActionResult> Index(
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
              public async Task<IActionResult> Create( int? personId, int? pageIndex, int? pageSize,
                     string? sortParam, ContactInfoVM? contactVM )
              {
                     //e-mail format validate
                     if ( !ModelState.IsValid )
                     {
                            return await ResultAsync( personId, pageIndex, pageSize, sortParam );
                     }

                     await this.service.AddAsync( contactVM );

                     return await ResultAsync( personId, pageIndex, pageSize, sortParam );
              }

              [HttpPost]
              public async Task<IActionResult> Edit(
                     [Required] int? personId, int? pageIndex, int? pageSize,
                     string? sortParam, List<ContactInfoVM>? entitiesForEdit )
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
                     [Required] int? personId, [Required] int? contactId,
                     int? pageIndex, int? pageSize, string? sortParam )
              {
                     if ( !ModelState.IsValid )
                     {
                            return await ResultAsync( personId, pageIndex, pageSize, sortParam );
                     }

                     await this.service.DeleteAsync( contactId, personId );

                     return await ResultAsync( personId, pageIndex, pageSize, sortParam );
              }

              //##############################################################

              private async Task<IActionResult> ResultAsync( int? personId, int? pageIndex, int? pageSize, string? sortParam )
              {
                     PagingListSorted<ContactInfoVM>? pagingSortedList =
                             await GetPagingListAsync( personId, pageIndex, pageSize, sortParam );

                     string? controllerName = this.RouteData.Values[ "controller" ].ToString();

                     pagingSortedList.RouteEdit = $"/{controllerName}/{nameof( Edit )}";

                     return View( "Index", pagingSortedList );
              }

              private async Task<PagingListSorted<ContactInfoVM>>? GetPagingListAsync
                     ( int? personId, int? pageIndex, int? pageSize, string? sortParam )
              {
                     IQueryable<ContactInfoVM>? sortedContactsList = this.service.AllNotDeleted( personId, sortParam );

                     if ( sortedContactsList == null )
                            return EmptyContactsSortedCollection( personId );

                     PagingListSorted<ContactInfoVM>? sortedList =
                            await PagingListSorted<ContactInfoVM>.CreateSortedCollectionAsync
                               ( sortedContactsList, pageIndex ?? _pageIndex, pageSize ?? _pageSize, sortParam );

                     if ( sortedList.ItemsCollection.Count == 0 )
                            return EmptyContactsSortedCollection( personId );

                     return sortedList;
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

              private PagingListSorted<ContactInfoVM> EmptyContactsSortedCollection( int? personId )
              {
                     List<ContactInfoVM> defaultContactInfoList = new List<ContactInfoVM>()
                     {
                            new ContactInfoVM{PersonId = personId }
                     };

                     PagingListSorted<ContactInfoVM> emptyPaginatedList = new PagingListSorted<ContactInfoVM>
                            ( defaultContactInfoList, this._count, this._pageIndex, this._pageSize, string.Empty );

                     return emptyPaginatedList;
              }
       }
}
