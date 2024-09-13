using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Services;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PersonViewModels;

namespace PersonnelWebApp.Controllers
{
       public class PersonsController : Controller
       {
              private const int PageSize = 2;
              private const int PageIndex = 1;
              private const int Count = 1;

              private readonly IPerson personService;

              public PersonsController( IPerson personService )
              {
                     this.personService = personService;
              }

              [HttpPost]
              public async Task<IActionResult> AllPersons( int? pageIndex, int? pageSize, string? sortParam )
              {
                     if ( !ModelState.IsValid )
                     {
                            return View( EmptyPersonsSortedCollection() );
                     }

                     IQueryable<PersonViewModel>? sortedPersonsList = this.personService.All( sortParam );

                     SortedPaginatedList<PersonViewModel>? sortedList =
                            await SortedPaginatedList<PersonViewModel>.CreateSortedCollectionAsync
                               ( sortedPersonsList, pageIndex ?? PageIndex, pageSize ?? PageSize, sortParam );

                     if ( sortedList.ItemsCollection.Count == 0 )
                     {
                            return View( EmptyPersonsSortedCollection() );
                     }

                     return View( sortedList );
              }

              [HttpPost]
              public async Task<IActionResult> AddPerson( PersonViewModel personVM )
              {
                     // TODO : validationServise if necessary

                     if ( !ModelState.IsValid )
                     {
                            return RedirectToAction( nameof( AllPersons ) );
                     }

                     await this.personService.AddAsync( personVM );

                     List<PersonViewModel> persons = await this.personService.All().ToListAsync();

                     return View( persons );
              }

              [HttpPost]
              public async Task<IActionResult> UpdatePerson( PersonViewModel personVM )
              {
                     // TODO : validationServise if necessary

                     if ( !ModelState.IsValid )
                     {
                            return RedirectToAction( nameof( AllPersons ) );
                     }

                     await this.personService.UpdateAsync( personVM );

                     List<PersonViewModel> persons = await this.personService.All().ToListAsync();

                     return View( persons );
              }

              //##############################################################

              private SortedPaginatedList<PersonViewModel> EmptyPersonsSortedCollection()
              {
                     List<PersonViewModel> defaultPersonsList = new List<PersonViewModel>()
                     {
                            new PersonViewModel()
                     };

                     SortedPaginatedList<PersonViewModel> emptyPaginatedList =
                            new SortedPaginatedList<PersonViewModel>( defaultPersonsList, Count, PageIndex, string.Empty );

                     return emptyPaginatedList;
              }
       }
}
