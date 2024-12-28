using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Services.ServiceContracts;
using Payroll.Services.UtilitiesServices.EntityValidateServices;
using Payroll.ViewModels;
using Payroll.ViewModels.PagingViewModels;
using Payroll.ViewModels.PersonViewModels;
using PersonnelWebApp.Utilities;

namespace PersonnelWebApp.Controllers
{
       public class AddressesController : Controller
       {
              private int _pageSize;
              private int _pageIndex;

              private IAddressService service;
              private IConfigurationRoot? privateConfig;
              private IValidate<ValidateBaseModel> validateService;

              public AddressesController( IAddressService addressService, IPrivateConfiguration configuration,
                     [FromKeyedServices( "AddressValidate" )] IValidate<ValidateBaseModel> validateService )
              {
                     this.service = addressService;

                     this.privateConfig = configuration.PrivateConfig();

                     this.validateService = validateService;

                     SetPagingVariables();
              }

              [HttpPost]
              public async Task<IActionResult> Index(
                     [Required(ErrorMessage ="Select person from person's table ! Go back to 'Persons List' .")]
                     int? personId, int? pageIndex, int? pageSize, string? sortParam, SearchAddressVM? filter )
              {
                     if ( !ModelState.IsValid )
                            return await ResultAsync( personId, pageIndex, pageSize, sortParam, filter );

                     return await ResultAsync( personId, pageIndex, pageSize, sortParam, filter );
              }

              [HttpPost]
              public async Task<IActionResult> Create(
                     [Required(ErrorMessage ="Select person from person's table ! Go back to 'Persons List' .")]
                     int? personId,
                     int? pageIndex, int? pageSize, string? sortParam, SearchAddressVM? filter, AddressVM? addressVM )
              {
                     this.validateService.Validate( ModelState, addressVM, nameof( Create ) );

                     if ( !ModelState.IsValid )
                            return await ResultAsync( personId, pageIndex, pageSize, sortParam, filter );

                     if ( !string.IsNullOrEmpty( addressVM.Country ) )
                            await this.service.AddAsync( addressVM );

                     return await ResultAsync( personId, pageIndex, pageSize, sortParam, filter );
              }

              [HttpPost]
              public async Task<IActionResult> CreateAttach(
                     [Required(ErrorMessage ="Select person from person's table ! Go back to 'Persons List' .")]
                     int? personId,
                     int? pageIndex, int? pageSize, string? sortParam, SearchAddressVM? filter, AddressVM? addressVM )
              {
                     this.validateService.Validate( ModelState, addressVM, nameof( CreateAttach ) );

                     if ( !ModelState.IsValid )
                            return await ResultAsync( personId, pageIndex, pageSize, sortParam, filter );

                     if ( !string.IsNullOrEmpty( addressVM.Country ) )
                            await this.service.AddAndAttachAsync( addressVM );

                     return await ResultAsync( personId, pageIndex, pageSize, sortParam, filter );
              }

              [HttpPost]
              public async Task<IActionResult> Edit(
                     [Required(ErrorMessage ="Select person from person's table ! Go back to 'Persons List' .")]
                     int? personId,
                     int? pageIndex, int? pageSize, string? sortParam, SearchAddressVM? filter, AddressVM? addressVM )
              {
                     this.validateService.Validate( ModelState, addressVM, nameof( Edit ) );

                     if ( !ModelState.IsValid )
                            return await ResultAsync( personId, pageIndex, pageSize, sortParam, filter );

                     if ( addressVM.Id > 0 )
                            await this.service.UpdateAsync( addressVM );

                     return await ResultAsync( personId, pageIndex, pageSize, sortParam, filter );
              }

              [HttpPost]
              public async Task<IActionResult> EditAttach(
                     [Required(ErrorMessage ="Select person from person's table ! Go back to 'Persons List' .")]
                     int? personId,
                     int? pageIndex, int? pageSize, string? sortParam, SearchAddressVM? filter, AddressVM? addressVM )
              {
                     this.validateService.Validate( ModelState, addressVM, nameof( EditAttach ) );

                     if ( !ModelState.IsValid )
                            return await ResultAsync( personId, pageIndex, pageSize, sortParam, filter );

                     if ( addressVM.Id > 0 )
                            await this.service.UpdateAndAttachAsync( addressVM );

                     return await ResultAsync( personId, pageIndex, pageSize, sortParam, filter );
              }

              [HttpPost]
              public async Task<IActionResult> Attach(
                     [Required(ErrorMessage ="Select person from person's table ! Go back to 'Persons List' .")]
                     int? personId,
                     int? pageIndex, int? pageSize, string? sortParam, SearchAddressVM? filter,
                     [Required(ErrorMessage ="Select an address from the table with addresses.")]
                     int? addressId,
                     [Required(ErrorMessage ="Select an address type from the check boxes.")]
                     string? addressType )
              {
                     if ( !ModelState.IsValid )
                            return await ResultAsync( personId, pageIndex, pageSize, sortParam, filter );

                     await this.service.AttachAddressAsync( personId, addressId, addressType );

                     return await ResultAsync( personId, pageIndex, pageSize, sortParam, filter );
              }

              [HttpPost]
              public async Task<IActionResult> Detach(
                     [Required(ErrorMessage ="Select person from person's table ! Go back to 'Persons List' .")]
                     int? personId,
                     int? pageIndex, int? pageSize, string? sortParam, SearchAddressVM? filter,
                     [Required(ErrorMessage ="Select an address type from the check boxes.")]
                     string? addressType )
              {
                     if ( !ModelState.IsValid )
                            return await ResultAsync( personId, pageIndex, pageSize, sortParam, filter );

                     await this.service.DetachAddressAsync( personId, addressType );

                     return await ResultAsync( personId, pageIndex, pageSize, sortParam, filter );
              }

              [HttpPost]
              public async Task<IActionResult> GetAddresses( [FromBody] SearchAddressVM? filterAddressVM )
              {
                     if ( !ModelState.IsValid )
                     {
                            return Json( ModelState );
                     }

                     IQueryable<AddressVM>? sortedAddressesList = this.service.AllAddresses( string.Empty, filterAddressVM );

                     List<AddressStringVM>? addressesList = await sortedAddressesList
                                                                                                        .Select( x => new AddressStringVM
                                                                                                        {
                                                                                                               Id = x.Id,
                                                                                                               Address = x.ToString()
                                                                                                        } )
                                                                                                        .ToListAsync();

                     return Json( addressesList );
              }

              [HttpGet]
              public async Task<IActionResult> GetFullAddress( int? id, string? returnType )
              {
                     if ( id == null || id < 1 )
                     {
                            return Json( string.Empty );
                     }

                     if ( !string.IsNullOrEmpty( returnType ) && returnType.Equals( "string" ) )
                     {
                            string? fullAddress = await this.service.GetEntity( id ).FirstOrDefaultAsync();

                            return Json( fullAddress );
                     }

                     AddressVM? addressVMObject = await this.service.GetEntity<AddressVM>( id ).FirstOrDefaultAsync();

                     return Json( addressVMObject );
              }

              [HttpPost]
              public async Task<IActionResult> Add( [FromBody] AddressVM? addressVM )
              {
                     this.validateService.Validate( ModelState, addressVM, nameof( Add ) );

                     if ( !ModelState.IsValid )
                     {
                            return Json( ModelState );
                     }

                     try
                     {
                            await this.service.AddAsync( addressVM );
                     }
                     catch ( Exception ex )
                     {
                            return Json( $"Error : {ex.Message} !" );
                     }

                     string operationIsSuccessful = "' Create ' operation was successful !";

                     return Json( operationIsSuccessful );
              }

              [HttpPost]
              public async Task<IActionResult> Update( [FromBody] AddressVM? addressVM )
              {
                     this.validateService.Validate( ModelState, addressVM, nameof( Update ) );

                     if ( !ModelState.IsValid )
                     {
                            return Json( ModelState );
                     }

                     try
                     {
                            await this.service.UpdateAsync( addressVM );
                     }
                     catch ( Exception ex )
                     {
                            return Json( $"Error : {ex.Message} !" );
                     }

                     string operationIsSuccessful = "' Edit ' operation was successful !";

                     return Json( operationIsSuccessful );
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
