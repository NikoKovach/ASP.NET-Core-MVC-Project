using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.UtilitiesServices
{
       public class DiplomasCollectionFactory : IDiplomasCollectionFactory
       {
              private IMapEntity mapper;
              private IRepository<Diploma> ropository;
              private Dictionary<string, IQueryable<DiplomaVM>> sortedDiplomasCollection;

              public DiplomasCollectionFactory( IMapEntity mapper, IRepository<Diploma> diplomasRopository )
              {
                     this.mapper = mapper;

                     this.ropository = diplomasRopository;
              }

              public IQueryable<DiplomaVM>? SortedCollection( int? personId, string? sortParam )
              {
                     IQueryable<Diploma>? personDiplomas = this.ropository
                                                                                           .AllAsNoTracking()
                                                                                           .Where( x => x.PersonId == personId &&
                                                                                                                   x.HasBeenDeleted == false );

                     SetDiplomasDictionary( personDiplomas );

                     if ( string.IsNullOrEmpty( sortParam ) )
                     {
                            return this.sortedDiplomasCollection[ "Default" ];
                     }

                     return this.sortedDiplomasCollection[ sortParam ];
              }

              //#######################################################################
              private void SetDiplomasDictionary( IQueryable<Diploma>? personDiplomas )
              {
                     this.sortedDiplomasCollection = new Dictionary<string, IQueryable<DiplomaVM>>();

                     this.sortedDiplomasCollection[ "Education_desc" ] = EducationDesc( personDiplomas );
                     this.sortedDiplomasCollection[ "Education_asc" ] = EducationAsc( personDiplomas );
                     this.sortedDiplomasCollection[ "RegNumber_desc" ] = RegNumberDesc( personDiplomas );
                     this.sortedDiplomasCollection[ "RegNumber_asc" ] = RegNumberAsc( personDiplomas );
                     this.sortedDiplomasCollection[ "Default" ] = DefaultPersonDiplomas( personDiplomas );
              }

              private IQueryable<DiplomaVM>? DefaultPersonDiplomas( IQueryable<Diploma>? personDiplomas )
              {
                     var diplomasCollection = mapper.ProjectTo<Diploma, DiplomaVM>( personDiplomas );

                     return diplomasCollection;
              }

              private IQueryable<DiplomaVM> EducationDesc( IQueryable<Diploma>? personDiplomas )
              {
                     var diplomas = this.mapper
                                                     .ProjectTo<Diploma, DiplomaVM>( personDiplomas )
                                                     .OrderByDescending( x => x.EducationTypeName );

                     return diplomas;
              }

              private IQueryable<DiplomaVM> EducationAsc( IQueryable<Diploma>? personDiplomas )
              {
                     var diplomas = this.mapper
                                                     .ProjectTo<Diploma, DiplomaVM>( personDiplomas )
                                                     .OrderBy( x => x.EducationTypeName );

                     return diplomas;
              }

              private IQueryable<DiplomaVM> RegNumberDesc( IQueryable<Diploma>? personDiplomas )
              {
                     var diplomas = this.mapper
                                                     .ProjectTo<Diploma, DiplomaVM>( personDiplomas )
                                                     .OrderByDescending( x => x.DiplomaRegNumber );

                     return diplomas;
              }

              private IQueryable<DiplomaVM> RegNumberAsc( IQueryable<Diploma>? personDiplomas )
              {
                     var diplomas = this.mapper
                                                     .ProjectTo<Diploma, DiplomaVM>( personDiplomas )
                                                     .OrderBy( x => x.DiplomaRegNumber );

                     return diplomas;
              }
       }
}

