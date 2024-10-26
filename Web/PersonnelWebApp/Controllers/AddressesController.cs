using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PagingViewModels;
using Payroll.ViewModels.PersonViewModels;
using PersonnelWebApp.Utilities;

namespace PersonnelWebApp.Controllers
{
       public class AddressesController : Controller
       {
              private int _pageSize;
              private int _pageIndex;
              //private int _count;

              private IAddressService service;
              private IConfigurationRoot? privateConfig;

              public AddressesController( IAddressService addressService, IPrivateConfiguration configuration )
              {
                     this.service = addressService;

                     this.privateConfig = configuration.PrivateConfig();

                     SetPagingVariables();
              }

              [HttpPost]
              public async Task<IActionResult> Index(
                     [Required(ErrorMessage ="Select person from person's table ! Go back to 'Persons List .'")]
                     int? personId, int? pageIndex, int? pageSize, string? sortParam, SearchAddressVM? filter )
              {

                     if ( !ModelState.IsValid )
                            return await ResultAsync( personId, pageIndex, pageSize, sortParam, filter );

                     return await ResultAsync( personId, pageIndex, pageSize, sortParam, filter );
              }

              // POST: ModelesController/Create
              [HttpPost]
              public async Task<IActionResult> Create( AddressVM? addressVM )
              {
                     //if ( !ModelState.IsValid )
                     //       return await ResultAsync( addressVM.PersonId );

                     //await this.service.AddAsync( addressVM );

                     //return await ResultAsync( addressVM.PersonId );

                     return View();
              }

              // POST: ModelesController/Edit/
              [HttpPost]
              public async Task<IActionResult> Edit( AddressVM? addressVM )
              {
                     //if ( !ModelState.IsValid )
                     //       return await ResultAsync( addressVM.PersonId );

                     //await this.service.UpdateAsync( addressVM );

                     //return await ResultAsync( addressVM.PersonId );

                     return View();
              }

              // POST: ModelesController/Delete/
              [HttpPost]
              public ActionResult Delete( int id )
              {
                     return View();
              }

              //################################################################

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

                     //if ( !string.IsNullOrEmpty( this.privateConfig[ "Paging:Count" ] ) )
                     //{
                     //       this._count = int.Parse( this.privateConfig[ "Paging:Count" ] );
                     //}
              }

              private async Task<IActionResult> ResultAsync( int? personId, int? pageIndex, int? pageSize,
                     string? sortParam, SearchAddressVM? filter )
              {
                     AddressesOfPersonVM? addressViewModel = await this.service.AllRealAsync( personId );

                     IQueryable<AddressVM>? sortedAddressesList = this.service.AllAddresses( sortParam, filter );

                     PagingListSortedFiltered<AddressVM, SearchAddressVM>? sortedList =
                     await PagingListSortedFiltered<AddressVM, SearchAddressVM>.CreateSortedCollectionAsync
                     ( sortedAddressesList, pageIndex ?? _pageIndex, pageSize ?? _pageSize, sortParam, filter );

                     addressViewModel.Addresses = sortedList;

                     return View( "Index", addressViewModel );
              }
       }
}
