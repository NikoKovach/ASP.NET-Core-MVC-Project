using Microsoft.AspNetCore.Mvc;
using Payroll.Services.Services;
using Payroll.Services.Services.ServiceContracts;
using Payroll.Services.UtilitiesServices.EntityValidateServices;
using Payroll.ViewModels;
using Payroll.ViewModels.PersonViewModels;

namespace PersonnelWebApp.Controllers
{
       public class PersonsController : Controller
       {
              private const int PageSize = 3;
              private const int PageIndex = 1;
              private const int Count = 1;

              private readonly IPersonService personService;
              private readonly IValidate<ValidateBaseModel> validateService;

              public PersonsController( IPersonService personService,
                     [FromKeyedServices( "PersonValidate" )] IValidate<ValidateBaseModel> validateService )
              {
                     this.personService = personService;

                     this.validateService = validateService;
              }

              [HttpPost]
              public async Task<IActionResult> AllPersons( int? pageIndex, int? pageSize, string? sortParam,
                                                                                               PersonFilterVM? filter )
              {
                     if ( !ModelState.IsValid )
                     {
                            return await ResultAsync( pageIndex, pageSize, sortParam, new PersonFilterVM() );

                            //PagingListSortedFiltered<PersonVM, PersonFilterVM>? sortedListWithErrors =
                            //await GetPersonsListOfPagesAsync( pageIndex, pageSize, sortParam, new PersonFilterVM() );

                            //return View( sortedListWithErrors );
                     }

                     return await ResultAsync( pageIndex, pageSize, sortParam, filter );
              }

              [HttpPost]
              public async Task<IActionResult> Create( int? pageIndex, int? pageSize, string? sortParam,
                     PersonFilterVM? filter, PersonVM? personVM )
              {
                     this.validateService.Validate( ModelState, personVM );

                     if ( !ModelState.IsValid )
                     {
                            return await ResultAsync( pageIndex, pageSize, sortParam, filter );
                     }

                     await this.personService.AddAsync( personVM );

                     return await ResultAsync( pageIndex, pageSize, sortParam, filter );
              }

              [HttpPost]
              public async Task<IActionResult> Edit( int? pageIndex, int? pageSize, string? sortParam,
                     PersonFilterVM? filter, List<PersonVM>? entitiesForEdit )
              {
                     this.validateService.Validate( ModelState, entitiesForEdit );

                     if ( !ModelState.IsValid )
                     {
                            if ( entitiesForEdit.Count > 0 )
                            {
                                   return Json( ModelState );
                            }

                            return await ResultAsync( pageIndex, pageSize, sortParam, filter );
                     }

                     await this.personService.UpdateAsync( entitiesForEdit );

                     if ( entitiesForEdit.Count > 0 )
                     {
                            return Json( ModelState );
                     }

                     return await ResultAsync( pageIndex, pageSize, sortParam, filter );
              }

              //##############################################################

              private async Task<IActionResult> ResultAsync( int? pageIndex, int? pageSize, string? sortParam, PersonFilterVM? filter )
              {
                     PagingListSortedFiltered<PersonVM, PersonFilterVM>? sortedList =
                           await GetPersonsListOfPagesAsync( pageIndex, pageSize, sortParam, filter );

                     string? controllerName = this.RouteData.Values[ "controller" ].ToString();

                     sortedList.RouteEdit = $"/{controllerName}/{nameof( Edit )}";

                     return View( "AllPersons", sortedList );
              }

              private PagingListSortedFiltered<PersonVM, PersonFilterVM> EmptyPersonsSortedCollection()
              {
                     List<PersonVM> defaultPersonsList = new List<PersonVM>()
                     {
                            new PersonVM()
                     };

                     PersonFilterVM personsFilter = new PersonFilterVM();

                     PagingListSortedFiltered<PersonVM, PersonFilterVM> emptyPaginatedList =
                            new PagingListSortedFiltered<PersonVM, PersonFilterVM>
                            ( defaultPersonsList, Count, PageIndex, PageSize, string.Empty, personsFilter );

                     return emptyPaginatedList;
              }

              private async Task<PagingListSortedFiltered<PersonVM, PersonFilterVM>>? GetPersonsListOfPagesAsync
                     ( int? pageIndex, int? pageSize, string? sortParam, PersonFilterVM? filter )
              {
                     IQueryable<PersonVM>? sortedPersonsList = this.personService.All( sortParam, filter );

                     PagingListSortedFiltered<PersonVM, PersonFilterVM>? sortedList =
                            await PagingListSortedFiltered<PersonVM, PersonFilterVM>.CreateSortedCollectionAsync
                               ( sortedPersonsList, pageIndex ?? PageIndex, pageSize ?? PageSize, sortParam, filter );

                     if ( sortedList.ItemsCollection.Count == 0 )
                            return EmptyPersonsSortedCollection();

                     return sortedList;
              }
       }
}

