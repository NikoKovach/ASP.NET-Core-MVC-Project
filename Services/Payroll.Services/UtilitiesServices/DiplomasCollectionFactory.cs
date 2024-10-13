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
              private IQueryable<Diploma> diplomasCollection;

              public DiplomasCollectionFactory( IMapEntity mapper, IRepository<Diploma> diplomasRopository )
              {
                     this.mapper = mapper;

                     this.diplomasCollection = diplomasRopository.AllAsNoTracking();

                     SetDiplomasDictionary();
              }

              public Dictionary<string, IQueryable<DiplomaVM>> SortedDiplomasCollection { get; set; }

              //#######################################################################
              private void SetDiplomasDictionary()
              {
                     this.SortedDiplomasCollection = new Dictionary<string, IQueryable<DiplomaVM>>();

                     this.SortedDiplomasCollection[ "Education_desc" ] = EducationDesc();
                     this.SortedDiplomasCollection[ "Education_asc" ] = EducationAsc();
                     this.SortedDiplomasCollection[ "RegNumber_desc" ] = RegNumberDesc();
                     this.SortedDiplomasCollection[ "RegNumber_asc" ] = RegNumberAsc();
              }

              private IQueryable<DiplomaVM> EducationDesc()
              {
                     var diplomas = this.mapper
                                                     .ProjectTo<Diploma, DiplomaVM>( this.diplomasCollection )
                                                     .OrderByDescending( x => x.EducationTypeName );

                     return diplomas;
              }

              private IQueryable<DiplomaVM> EducationAsc()
              {
                     var diplomas = this.mapper
                                                     .ProjectTo<Diploma, DiplomaVM>( this.diplomasCollection )
                                                     .OrderBy( x => x.EducationTypeName );

                     return diplomas;
              }

              private IQueryable<DiplomaVM> RegNumberDesc()
              {
                     var diplomas = this.mapper
                                                     .ProjectTo<Diploma, DiplomaVM>( this.diplomasCollection )
                                                     .OrderByDescending( x => x.DiplomaRegNumber );

                     return diplomas;
              }

              private IQueryable<DiplomaVM> RegNumberAsc()
              {
                     var diplomas = this.mapper
                                                     .ProjectTo<Diploma, DiplomaVM>( this.diplomasCollection )
                                                     .OrderBy( x => x.DiplomaRegNumber );

                     return diplomas;
              }
       }
}

