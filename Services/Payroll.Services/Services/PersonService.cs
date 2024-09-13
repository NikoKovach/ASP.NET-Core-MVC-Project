using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.Services.UtilitiesServices;
using Payroll.ViewModels;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services
{
       public class PersonService : IPerson
       {
              private IRepository<Person> repository;
              private IMapEntity mapper;

              public PersonService( IRepository<Person> personsRepo, IMapEntity mapper )
              {
                     repository = personsRepo;

                     this.mapper = mapper;
              }

              public IQueryable<PersonViewModel> All()
              {
                     IQueryable<PersonViewModel> persons = mapper
                            .ProjectTo<Person, PersonViewModel>( repository.AllAsNoTracking() )
                            .OrderBy( x => x.FirstName )
                            .ThenBy( x => x.LastName );

                     return persons;
              }

              public IQueryable<PersonViewModel> All( string? sortParam )
              {
                     if ( string.IsNullOrEmpty( sortParam ) )
                            return this.All();

                     IQueryable<PersonViewModel> persons =
                                                                      new SortPersonsFactory( mapper, repository.AllAsNoTracking() )
                                                                      .Sort( sortParam );

                     return persons;
              }

              public IQueryable<SearchPersonVM> AllActive_SearchPersonVM()
              {
                     var persons = mapper
                            .ProjectTo<Person, SearchPersonVM>( repository.AllAsNoTracking() )
                            .OrderBy( x => x.FirstName )
                            .ThenBy( x => x.LastName );

                     return persons;
              }

              public async Task AddAsync( PersonViewModel viewModel )
              {
                     Person? person = mapper.Map<PersonViewModel, Person>( viewModel );

                     await repository.AddAsync( person );

                     await repository.SaveChangesAsync();
              }

              public async Task UpdateAsync( PersonViewModel viewModel )
              {
                     Person? person = mapper.Map<PersonViewModel, Person>( viewModel );

                     repository.Update( person );

                     await repository.SaveChangesAsync();
              }
       }
}
