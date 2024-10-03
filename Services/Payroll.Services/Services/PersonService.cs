using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.Services.UtilitiesServices;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services
{
       public class PersonService : IPersonService
       {
              private IRepository<Person> repository;
              private IMapEntity mapper;

              public PersonService( IRepository<Person> personsRepo, IMapEntity mapper )
              {
                     repository = personsRepo;

                     this.mapper = mapper;
              }

              public IQueryable<PersonVM> All()
              {
                     IQueryable<PersonVM> persons = mapper
                            .ProjectTo<Person, PersonVM>( repository.AllAsNoTracking() )
                            .OrderBy( x => x.FirstName )
                            .ThenBy( x => x.LastName );

                     return persons;
              }

              public IQueryable<PersonVM> All( string? sortParam, PersonFilterVM? filter )
              {
                     if ( filter.PersonId == null && string.IsNullOrEmpty( filter.SearchName )
                            && string.IsNullOrEmpty( filter.CivilID ) )
                            filter = null;

                     if ( string.IsNullOrEmpty( sortParam ) && filter == null )
                            return this.All();

                     PersonsCollectionFactory personsFactory =
                                    new PersonsCollectionFactory( mapper, repository.AllAsNoTracking() );

                     if ( filter == null )
                     {
                            IQueryable<PersonVM> sortedPersons =
                           personsFactory.SortedPersonsCollection[ sortParam ];

                            return sortedPersons;
                     }

                     IQueryable<PersonVM> filteredPersonsList = personsFactory.Filtrate( filter, sortParam );

                     return filteredPersonsList;
              }

              public IQueryable<SearchPersonVM> AllActive_SearchPersonVM()
              {
                     var persons = mapper
                            .ProjectTo<Person, SearchPersonVM>( repository.AllAsNoTracking() )
                            .OrderBy( x => x.FirstName )
                            .ThenBy( x => x.LastName );

                     return persons;
              }

              public async Task AddAsync( PersonVM viewModel )
              {
                     Person? person = mapper.Map<PersonVM, Person>( viewModel );

                     await repository.AddAsync( person );

                     await repository.SaveChangesAsync();
              }

              public async Task UpdateAsync( PersonVM viewModel )
              {
                     Person? person = mapper.Map<PersonVM, Person>( viewModel );

                     repository.Update( person );

                     await repository.SaveChangesAsync();
              }

              public async Task UpdateAsync( ICollection<PersonVM> viewModels )
              {
                     var personList = mapper.Map<List<PersonVM>, List<Person>>( viewModels.ToList() );

                     repository.Update( personList );

                     await repository.SaveChangesAsync();
              }
       }
}
