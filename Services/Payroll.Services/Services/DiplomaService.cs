using Microsoft.EntityFrameworkCore;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Models.EnumTables;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services
{
       public class DiplomaService : IDiplomaService
       {
              private IRepository<Diploma> repository;
              private IMapEntity mapper;
              private IFactorySortCollection<DiplomaVM> diplomaSortFactory;

              public DiplomaService( IRepository<Diploma> diplomasRepo, IMapEntity mapper,
                     IFactorySortCollection<DiplomaVM> diplomaSortFactory )
              {
                     repository = diplomasRepo;

                     this.mapper = mapper;

                     this.diplomaSortFactory = diplomaSortFactory;
              }

              public IQueryable<DiplomaVM>? All( int? personId )
              {
                     IQueryable<Diploma>? diplomas = repository.AllAsNoTracking()
                                                                                                      .Where( x => x.PersonId == personId );

                     IQueryable<DiplomaVM>? diplomasVM = mapper.ProjectTo<Diploma, DiplomaVM>( diplomas );

                     return diplomasVM;
              }

              public IQueryable<DiplomaVM>? AllNotDeleted( int? personId, string? sortParam )
              {
                     if ( personId == null )
                     {
                            return null;
                     }

                     IQueryable<DiplomaVM>? sortedDiplomas = this.diplomaSortFactory.SortedCollection( sortParam, personId );

                     return sortedDiplomas;
              }

              public async Task AddAsync( DiplomaVM? viewModel )
              {
                     Diploma? diploma = mapper.Map<DiplomaVM, Diploma>( viewModel );

                     await this.SetExistingEducationAsync( diploma, viewModel.EducationTypeName );

                     await this.repository.AddAsync( diploma );

                     await this.repository.SaveChangesAsync();
              }

              public async Task UpdateAsync( DiplomaVM viewModel )
              {
                     Diploma? diploma = mapper.Map<DiplomaVM, Diploma>( viewModel );

                     await this.SetExistingEducationAsync( diploma, viewModel.EducationTypeName );

                     this.repository.Update( diploma );

                     await this.repository.SaveChangesAsync();
              }

              public async Task UpdateAsync( ICollection<DiplomaVM> viewModels )
              {
                     List<DiplomaVM> diplomasVMList = viewModels.ToList();

                     List<Diploma>? diplomasList = mapper.Map<List<DiplomaVM>, List<Diploma>>( diplomasVMList );

                     for ( int i = 0; i < diplomasList.Count; i++ )
                     {
                            await this.SetExistingEducationAsync( diplomasList[ i ], diplomasVMList[ i ].EducationTypeName );
                     }

                     this.repository.Update( diplomasList );

                     await this.repository.SaveChangesAsync();
              }

              public async Task DeleteAsync( int? id, int? personId = null )
              {
                     if ( id == null || personId == null )
                     {
                            return;
                     }

                     Diploma? diploma = await this.repository.All()
                                                                                               .Where( x => x.Id == id && x.PersonId == personId )
                                                                                               .FirstOrDefaultAsync();
                     if ( personId == null )
                     {
                            diploma = await this.repository.All().Where( x => x.Id == id )
                                                                                             .FirstOrDefaultAsync();
                     }

                     diploma.HasBeenDeleted = true;
                     diploma.DeletionDate = DateTime.UtcNow;

                     await this.repository.SaveChangesAsync();
              }

              public IQueryable<string> TypesOfEducation()
              {
                     IQueryable<string>? educations = this.repository.Context.EducationTypes.Select( x => x.Type );

                     return educations;
              }

              //**************************************************************

              private async Task SetExistingEducationAsync( Diploma diploma, string? typeOfEducation )
              {
                     EducationType? educationType = await this.repository.Context.EducationTypes
                                                                                        .Where( x => x.Type == typeOfEducation )
                                                                                        .FirstOrDefaultAsync();

                     if ( educationType != null && educationType.Id > 0 )
                     {
                            diploma.EducationType = educationType;
                     }
              }
       }
}


//if ( string.IsNullOrEmpty( sortParam ) )
//{
//       IQueryable<Diploma>? diplomas = repository.AllAsNoTracking()
//                                                                              .Where( x => x.PersonId == personId
//                                                                                       && x.HasBeenDeleted == false );

//       IQueryable<DiplomaVM> diplomasVM = mapper.ProjectTo<Diploma, DiplomaVM>( diplomas );

//       return diplomasVM;
//}

//DiplomaVMCollectionFactory diplomasVMFactory =
//               new DiplomaVMCollectionFactory( mapper, diplomas );

//IQueryable<DiplomaVM> sortedDiplomas = this.diplomaCollections.SortedDiplomasCollection[ sortParam ];


//public IQueryable<SearchPersonVM> AllActive_SearchPersonVM()
//{
//       var persons = mapper
//              .ProjectTo<Person, SearchPersonVM>( repository.AllAsNoTracking() )
//              .OrderBy( x => x.FirstName )
//              .ThenBy( x => x.LastName );

//       return persons;
//}


//if ( filter.PersonId == null && string.IsNullOrEmpty( filter.SearchName )
//       && string.IsNullOrEmpty( filter.CivilID ) )
//       filter = null;

//if ( string.IsNullOrEmpty( sortParam ) && filter == null )
//       return this.All();

//PersonsCollectionFactory personsFactory =
//               new PersonsCollectionFactory( mapper, repository.AllAsNoTracking() );

//if ( filter == null )
//{
//       IQueryable<PersonVM> sortedPersons =
//      personsFactory.SortedPersonsCollection[ sortParam ];

//       return sortedPersons;
//}