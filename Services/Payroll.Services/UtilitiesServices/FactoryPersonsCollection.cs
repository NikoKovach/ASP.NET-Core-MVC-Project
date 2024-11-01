using System.Reflection;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.UtilitiesServices
{
       public class FactoryPersonsCollection : IFactorySortCollection<PersonVM>
       {
              private IMapEntity mapper;
              private IRepository<Person> repository;
              private Dictionary<string, IQueryable<PersonVM>> sortedPersonsCollection;

              public FactoryPersonsCollection( IMapEntity mapper, IRepository<Person> personRepository )
              {
                     this.mapper = mapper;

                     this.repository = personRepository;
              }

              public IQueryable<PersonVM>? SortedCollection( string? sortParam, params object[] items )
              {
                     PersonFilterVM? filter = ( items.Length > 0 ) ? (PersonFilterVM?) items[ 0 ] : new PersonFilterVM();

                     IQueryable<Person>? persons = FilterPersons( filter );

                     SetPersonsDictionary( persons );

                     if ( string.IsNullOrEmpty( sortParam ) )
                     {
                            return this.sortedPersonsCollection[ "default" ];
                     }

                     return this.sortedPersonsCollection[ sortParam ];
              }

              //###################################################################

              private void SetPersonsDictionary( IQueryable<Person>? persons )
              {
                     this.sortedPersonsCollection = new Dictionary<string, IQueryable<PersonVM>>();

                     this.sortedPersonsCollection[ "FirstName_desc" ] = FirstNameDesc( persons );
                     this.sortedPersonsCollection[ "FirstName_asc" ] = FirstNameAsc( persons );
                     this.sortedPersonsCollection[ "LastName_desc" ] = LastNameDesc( persons );
                     this.sortedPersonsCollection[ "LastName_asc" ] = LastNameAsc( persons );
                     this.sortedPersonsCollection[ "CivilNumber_desc" ] = CivilNumberDesc( persons );
                     this.sortedPersonsCollection[ "CivilNumber_asc" ] = CivilNumberAsc( persons );

                     this.sortedPersonsCollection[ "default" ] = DefaultPersonsCollection( persons );
              }

              private IQueryable<PersonVM> DefaultPersonsCollection( IQueryable<Person>? persons )
              {
                     IQueryable<PersonVM>? defaultCollection = this.mapper.ProjectTo<Person, PersonVM>( persons )
                                                                                                                           .OrderBy( x => x.FirstName )
                                                                                                                           .ThenBy( x => x.LastName );

                     return defaultCollection;
              }

              private IQueryable<PersonVM> FirstNameDesc( IQueryable<Person>? persons )
              {
                     var personsList = this.mapper
                                                     .ProjectTo<Person, PersonVM>( persons )
                                                     .OrderByDescending( x => x.FirstName )
                                                     .ThenByDescending( x => x.LastName );

                     return personsList;
              }

              private IQueryable<PersonVM> FirstNameAsc( IQueryable<Person>? persons )
              {
                     var personsList = this.mapper
                                                     .ProjectTo<Person, PersonVM>( persons )
                                                     .OrderBy( x => x.FirstName )
                                                     .ThenBy( x => x.LastName );

                     return personsList;
              }

              private IQueryable<PersonVM> LastNameDesc( IQueryable<Person>? persons )
              {
                     var personsList = this.mapper
                                                     .ProjectTo<Person, PersonVM>( persons )
                                                     .OrderByDescending( x => x.LastName )
                                                     .ThenBy( x => x.FirstName );

                     return personsList;
              }

              private IQueryable<PersonVM> LastNameAsc( IQueryable<Person>? persons )
              {
                     var personsList = this.mapper
                                                     .ProjectTo<Person, PersonVM>( persons )
                                                     .OrderBy( x => x.LastName )
                                                     .ThenBy( x => x.FirstName );
                     return personsList;
              }

              private IQueryable<PersonVM> CivilNumberAsc( IQueryable<Person>? persons )
              {
                     var personsList = this.mapper
                                                     .ProjectTo<Person, PersonVM>( persons )
                                                     .OrderBy( x => x.CivilNumber );

                     return personsList;
              }

              private IQueryable<PersonVM> CivilNumberDesc( IQueryable<Person>? persons )
              {
                     var personsList = this.mapper
                                                     .ProjectTo<Person, PersonVM>( persons )
                                                     .OrderByDescending( x => x.CivilNumber );

                     return personsList;
              }

              private IQueryable<Person> FilterPersons( PersonFilterVM? filter )
              {
                     ParseSearchName( filter );

                     if ( HasData( filter ) )
                     {
                            IQueryable<Person>? collection = this.repository
                                                                                           .AllAsNoTracking()
                                                                                           .Where
                                                                                           ( x => x.FirstName.ToLower().StartsWith( filter.FirstName ) ||
                                                                                                      x.MiddleName.ToLower().StartsWith( filter.MiddleName ) ||
                                                                                                      x.LastName.ToLower().StartsWith( filter.LastName ) ||
                                                                                                      x.EGN.StartsWith( filter.CivilID ) ||
                                                                                                      x.Id == filter.PersonId
                                                                                           );

                            return collection;
                     }

                     return this.repository.AllAsNoTracking();
              }

              private void ParseSearchName( PersonFilterVM? filter )
              {
                     if ( string.IsNullOrEmpty( filter.SearchName ) )
                     {
                            filter.FirstName = null;
                            filter.MiddleName = null;
                            filter.LastName = null;

                            return;
                     }

                     string[] nameArray = filter.SearchName.Split( " ", 2, StringSplitOptions.RemoveEmptyEntries );

                     if ( nameArray.Length == 1 )
                     {
                            filter.LastName = nameArray[ 0 ].ToLower();
                            filter.FirstName = nameArray[ 0 ].ToLower();
                            filter.MiddleName = null;
                            return;
                     }

                     //###########################################################
                     filter.FirstName = nameArray[ 0 ].ToLower();

                     string[] middleLastNameArr = nameArray[ 1 ].Split( " ", 2, StringSplitOptions.RemoveEmptyEntries );

                     if ( middleLastNameArr.Length == 1 )
                     {
                            filter.LastName = middleLastNameArr[ 0 ].ToLower();
                            filter.MiddleName = null;
                            return;
                     }

                     filter.LastName = middleLastNameArr[ 1 ].ToLower();
                     filter.MiddleName = middleLastNameArr[ 0 ].ToLower();

                     return;
              }

              private bool HasData( PersonFilterVM? filter )
              {
                     PropertyInfo[]? propertiesList = filter.GetType().GetProperties();

                     List<object?>? propertyValues = propertiesList.Select( x => x.GetValue( filter ) ).ToList();

                     return propertyValues.Any( x => x != null );
              }
       }
}
