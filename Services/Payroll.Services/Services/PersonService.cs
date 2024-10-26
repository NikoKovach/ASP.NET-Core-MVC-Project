using Microsoft.EntityFrameworkCore;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Models.EnumTables;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services
{
       public class PersonService : IPersonService
       {
              private IRepository<Person> repository;
              private IMapEntity mapper;
              private IPersonsCollectionFactory collectionsFactory;

              public PersonService( IRepository<Person> personsRepo, IMapEntity mapper,
                                                        IPersonsCollectionFactory personsCollectionFactory )
              {
                     repository = personsRepo;

                     this.mapper = mapper;

                     this.collectionsFactory = personsCollectionFactory;
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

                     if ( filter == null )
                     {
                            IQueryable<PersonVM> sortedPersons =
                           this.collectionsFactory.SortedPersonsCollection[ sortParam ];

                            return sortedPersons;
                     }

                     IQueryable<PersonVM> filteredPersonsList = this.collectionsFactory.Filtrate( filter, sortParam );

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

                     await this.SetExistingGenderAsync( person, viewModel.GenderType );

                     await this.repository.AddAsync( person );

                     await this.repository.SaveChangesAsync();
              }

              public async Task UpdateAsync( PersonVM viewModel )
              {
                     Person? person = mapper.Map<PersonVM, Person>( viewModel );

                     await this.SetExistingGenderAsync( person, viewModel.GenderType );

                     this.repository.Update( person );

                     await this.repository.SaveChangesAsync();
              }

              public async Task UpdateAsync( ICollection<PersonVM> viewModels )
              {
                     List<PersonVM> viewModelsList = viewModels.ToList();

                     List<Person>? personList = mapper.Map<List<PersonVM>, List<Person>>( viewModelsList );

                     for ( int i = 0; i < personList.Count; i++ )
                     {
                            await SetExistingGenderAsync( personList[ i ], viewModelsList[ i ].GenderType );
                     }

                     this.repository.Update( personList );

                     await this.repository.SaveChangesAsync();
              }

              public async Task<string?> GetEntityNameAsync( int? entityId )
              {
                     Person? person = await this.repository.AllAsNoTracking()
                                                                                         .Where( x => x.Id == entityId )
                                                                                         .FirstOrDefaultAsync();

                     return person.FullName;
              }

              public IQueryable<string>? GenderTypes()
              {
                     IQueryable<string>? genders = this.repository.Context.Genders.Select( x => x.Type );

                     return genders;
              }

              //**************************************************************

              private async Task SetExistingGenderAsync( Person person, string? genderType )
              {
                     Gender? gender = await this.repository.Context.Genders
                                                                                        .Where( x => x.Type == genderType )
                                                                                        .FirstOrDefaultAsync();

                     if ( gender != null && gender.Id > 0 )
                     {
                            person.Gender = gender;
                     }
              }
       }
}
