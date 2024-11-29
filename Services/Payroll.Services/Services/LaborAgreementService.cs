using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.EmpContractViewModels;

namespace Payroll.Services.Services
{
       public class LaborAgreementService : ILaborAgreementService
       {
              private IRepository<Employee> empRepository;
              private IMapEntity mapper;
              private IFactorySortCollection<LaborAgreementVM> agreementsFactory;

              public LaborAgreementService( IRepository<Employee> empRepository, IMapEntity mapper,
                     IFactorySortCollection<LaborAgreementVM> contractsFactory )
              {
                     this.empRepository = empRepository;

                     this.mapper = mapper;

                     this.agreementsFactory = contractsFactory;
              }

              public IQueryable<LaborAgreementVM>? All( int? companyId )
              {
                     var contracts = this.empRepository.AllAsNoTracking()
                                                 .Where( x => x.CompanyId == companyId
                                                                        && x.EmploymentContract.HasBeenDeleted == false
                                                                        && x.EmploymentContract.ContractType.Type.Equals( "Labor Contract" ) )
                                                 .Select<Employee, EmploymentContract>( x => x.EmploymentContract );

                     var empContractsVM = this.mapper.ProjectTo<EmploymentContract, LaborAgreementVM>( contracts );

                     return empContractsVM;
              }

              public IQueryable<LaborAgreementVM>? AllActive( int? companyId )
              {
                     IQueryable<LaborAgreementVM>? empContractsVM = this.All( companyId )
                                                                                                                               .Where( x => x.IsActive == true );

                     return empContractsVM;
              }

              public IQueryable<LaborAgreementVM>? AllActive( int? companyId, string? sortParam,
                                                                                                                        FilterAgreementVM? filter )
              {
                     IQueryable<LaborAgreementVM>? activeAgreements = this.AllActive( companyId );

                     if ( companyId != null && companyId > 0 || !string.IsNullOrEmpty( sortParam ) )
                     {
                            IQueryable<LaborAgreementVM>? resultAgreements =
                            this.agreementsFactory.SortedCollection( sortParam, filter, activeAgreements );

                            return resultAgreements;
                     }

                     return activeAgreements;
              }

              public Task AddAsync( LaborAgreementVM viewModel )
              {
                     throw new NotImplementedException();
              }

              public Task UpdateAsync( LaborAgreementVM viewModel )
              {
                     throw new NotImplementedException();
              }
       }
}


