using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.UtilitiesServices
{
       public class PersonsCollectionFactory
       {
              private IMapEntity mapper;
              private IQueryable<Person> personsCollection;

              public PersonsCollectionFactory( IMapEntity mapper, IQueryable<Person> personsList )
              {
                     this.mapper = mapper;

                     this.personsCollection = personsList;

                     SetPersonsDictionary();
              }

              public Dictionary<string, IQueryable<PersonVM>> SortedPersonsCollection { get; set; }

              public IQueryable<PersonVM> Filtrate( PersonFilterVM? filter, string? sort )
              {
                     this.personsCollection = Search( filter );

                     if ( string.IsNullOrEmpty( sort ) )
                     {
                            IQueryable<PersonVM> personsList = mapper
                                                                                          .ProjectTo<Person, PersonVM>( this.personsCollection )
                                                                                          .OrderBy( x => x.FirstName )
                                                                                          .ThenBy( x => x.LastName );

                            return personsList;
                     }

                     SetPersonsDictionary();

                     IQueryable<PersonVM> resultPersonsVM = SortedPersonsCollection[ sort ];

                     return resultPersonsVM;
              }

              private void SetPersonsDictionary()
              {
                     this.SortedPersonsCollection = new Dictionary<string, IQueryable<PersonVM>>();

                     this.SortedPersonsCollection[ "FirstName_desc" ] = FirstNameDesc();
                     this.SortedPersonsCollection[ "FirstName_asc" ] = FirstNameAsc();
                     this.SortedPersonsCollection[ "LastName_desc" ] = LastNameDesc();
                     this.SortedPersonsCollection[ "LastName_asc" ] = LastNameAsc();
                     this.SortedPersonsCollection[ "CivilNumber_desc" ] = CivilNumberDesc();
                     this.SortedPersonsCollection[ "CivilNumber_asc" ] = CivilNumberAsc();
              }

              private IQueryable<PersonVM> FirstNameDesc()
              {
                     var persons = this.mapper
                                                     .ProjectTo<Person, PersonVM>( this.personsCollection )
                                                     .OrderByDescending( x => x.FirstName )
                                                     .ThenByDescending( x => x.LastName );
                     return persons;
              }

              private IQueryable<PersonVM> FirstNameAsc()
              {
                     var persons = this.mapper
                                                     .ProjectTo<Person, PersonVM>( this.personsCollection )
                                                     .OrderBy( x => x.FirstName )
                                                     .ThenBy( x => x.LastName );

                     return persons;
              }

              private IQueryable<PersonVM> LastNameDesc()
              {
                     var persons = this.mapper
                                                     .ProjectTo<Person, PersonVM>( this.personsCollection )
                                                     .OrderByDescending( x => x.LastName )
                                                     .ThenBy( x => x.FirstName );

                     return persons;
              }

              private IQueryable<PersonVM> LastNameAsc()
              {
                     var persons = this.mapper
                                                     .ProjectTo<Person, PersonVM>( this.personsCollection )
                                                     .OrderBy( x => x.LastName )
                                                     .ThenBy( x => x.FirstName );
                     return persons;
              }

              private IQueryable<PersonVM> CivilNumberAsc()
              {
                     var persons = this.mapper
                                                     .ProjectTo<Person, PersonVM>( this.personsCollection )
                                                     .OrderBy( x => x.CivilNumber );

                     return persons;
              }

              private IQueryable<PersonVM> CivilNumberDesc()
              {
                     var persons = this.mapper
                                                     .ProjectTo<Person, PersonVM>( this.personsCollection )
                                                     .OrderByDescending( x => x.CivilNumber );

                     return persons;
              }

              private IQueryable<Person> Search( PersonFilterVM? filter )
              {
                     ParseSearchName( filter );
                     int? searcedID = filter.PersonId;
                     string? civilId = filter.CivilID;
                     string? firstName = filter.FirstName;
                     string? middleName = filter.MiddleName;
                     string? lastName = filter.LastName;

                     IQueryable<Person>? collection = this.personsCollection
                                                                                        .Where
                                                                                        ( x => x.FirstName.ToLower().StartsWith( firstName ) ||
                                                                                                   x.MiddleName.ToLower().StartsWith( middleName ) ||
                                                                                                   x.LastName.ToLower().StartsWith( lastName ) ||
                                                                                                   x.EGN.StartsWith( civilId ) ||
                                                                                                   x.Id == searcedID
                                                                                        );

                     return collection;
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
                            filter.LastName = nameArray[ 0 ];
                            filter.FirstName = nameArray[ 0 ];
                            filter.MiddleName = null;
                            return;
                     }
                     //###########################################################
                     filter.FirstName = nameArray[ 0 ];

                     string[] middleLastNameArr = nameArray[ 1 ].Split( " ", 2, StringSplitOptions.RemoveEmptyEntries );

                     if ( middleLastNameArr.Length == 1 )
                     {
                            filter.LastName = middleLastNameArr[ 0 ];
                            filter.MiddleName = null;
                            return;
                     }

                     filter.LastName = middleLastNameArr[ 1 ];
                     filter.MiddleName = middleLastNameArr[ 0 ];

                     return;
              }
       }
}
