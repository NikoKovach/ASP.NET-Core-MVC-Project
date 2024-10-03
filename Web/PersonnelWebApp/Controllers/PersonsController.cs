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
              public async Task<IActionResult> AllPersons( int? pageIndex, int? pageSize,
                     string? sortParam, PersonFilterVM? filter, PersonVM? personVM, List<PersonVM>? entitiesForEdit )
              {
                     this.validateService.Validate( ModelState, personVM );

                     this.validateService.Validate( ModelState, entitiesForEdit );

                     if ( !ModelState.IsValid )
                     {
                            if ( entitiesForEdit.Count > 0 )
                            {
                                   return Json( ModelState );
                            }

                            SortedPaginatedList<PersonVM, PersonFilterVM>? sortedListWithErrors =
                            await GetPersonsListOfPagesAsync( pageIndex, pageSize, sortParam, new PersonFilterVM() );

                            return View( sortedListWithErrors );
                     }

                     if ( !string.IsNullOrEmpty( personVM.FirstName )
                            && !string.IsNullOrEmpty( personVM.LastName )
                            && !string.IsNullOrEmpty( personVM.CivilNumber )
                            && !string.IsNullOrEmpty( personVM.GenderType ) )
                     {
                            await this.personService.AddAsync( personVM );
                     }

                     if ( entitiesForEdit.Count > 0 )
                     {
                            await this.personService.UpdateAsync( entitiesForEdit );
                     }

                     SortedPaginatedList<PersonVM, PersonFilterVM>? sortedList =
                           await GetPersonsListOfPagesAsync( pageIndex, pageSize, sortParam, filter );

                     return View( sortedList );
              }

              //##############################################################

              private SortedPaginatedList<PersonVM, PersonFilterVM> EmptyPersonsSortedCollection()
              {
                     List<PersonVM> defaultPersonsList = new List<PersonVM>()
                     {
                            new PersonVM()
                     };

                     PersonFilterVM personsFilter = new PersonFilterVM();

                     SortedPaginatedList<PersonVM, PersonFilterVM> emptyPaginatedList =
                            new SortedPaginatedList<PersonVM, PersonFilterVM>
                            ( defaultPersonsList, Count, PageIndex, string.Empty, personsFilter );

                     return emptyPaginatedList;
              }

              private async Task<SortedPaginatedList<PersonVM, PersonFilterVM>>? GetPersonsListOfPagesAsync
                     ( int? pageIndex, int? pageSize, string? sortParam, PersonFilterVM? filter )
              {
                     IQueryable<PersonVM>? sortedPersonsList = this.personService.All( sortParam, filter );

                     SortedPaginatedList<PersonVM, PersonFilterVM>? sortedList =
                            await SortedPaginatedList<PersonVM, PersonFilterVM>.CreateSortedCollectionAsync
                               ( sortedPersonsList, pageIndex ?? PageIndex, pageSize ?? PageSize, sortParam, filter );

                     if ( sortedList.ItemsCollection.Count == 0 )
                            return EmptyPersonsSortedCollection();

                     return sortedList;
              }
       }
}


//[HttpPost]
//public async Task<IActionResult> AddPerson( PersonVM? personVM )
//{
//       this.validateService.Validate( ModelState, personVM );

//       if ( !ModelState.IsValid )
//       {
//              return RedirectToAction( nameof( AllPersons ) );
//       }

//       await this.personService.AddAsync( personVM );


//List<PersonVM> persons = await this.personService.All().ToListAsync();

//return RedirectToAction( nameof( AllPersons ) );
//}

//[HttpPost]
//public async Task<IActionResult> UpdatePerson( PersonVM personVM )
//{
//       // TODO : validationServise if necessary

//       if ( !ModelState.IsValid )
//       {
//              return RedirectToAction( nameof( AllPersons ) );
//       }

//       await this.personService.UpdateAsync( personVM );

//       List<PersonVM> persons = await this.personService.All().ToListAsync();

//       return View( persons );
//}