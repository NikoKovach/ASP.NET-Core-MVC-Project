using Microsoft.EntityFrameworkCore;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.EmpContractViewModels;

namespace Payroll.Services.Services
{
       public class LaborAgreementService : ILaborAgreementService
       {
              private IRepository<EmploymentContract> repository;
              private IMapEntity mapper;
              private IFactorySortCollection<LaborAgreementVM> agreementsFactory;

              public LaborAgreementService( IRepository<EmploymentContract> empRepository, IMapEntity mapper,
                     IFactorySortCollection<LaborAgreementVM> contractsFactory )
              {
                     this.repository = empRepository;

                     this.mapper = mapper;

                     this.agreementsFactory = contractsFactory;
              }

              public IQueryable<LaborAgreementVM>? All( int? companyId )
              {
                     var contracts = this.repository.Context.Employees
                                                       .AsNoTracking()
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

              public IQueryable<LaborAgreementVM>? GetContract( int? contractId, int? companyId )
              {
                     IQueryable<LaborAgreementVM>? agreement = this.AllActive( companyId )
                                                                                                                   .Where( x => x.Id == contractId );

                     return agreement;
              }

              public async Task AddAsync( LaborAgreementVM agreementVM )
              {
                     EmploymentContract? contract = this.mapper
                                                                                        .Map<LaborAgreementVM, EmploymentContract>( agreementVM );

                     if ( contract != null )
                     {
                            await this.repository.AddAsync( contract );

                            await this.repository.SaveChangesAsync();
                     }
              }

              public async Task UpdateAsync( LaborAgreementVM agreementVM )
              {
                     EmploymentContract? contract = this.mapper
                                                                                        .Map<LaborAgreementVM, EmploymentContract>( agreementVM );

                     if ( contract != null )
                     {
                            this.repository.Update( contract );

                            await repository.SaveChangesAsync();
                     }
              }
       }
}


