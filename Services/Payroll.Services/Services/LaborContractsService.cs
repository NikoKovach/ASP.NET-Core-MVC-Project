using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.LaborContractViewModels;

namespace Payroll.Services.Services
{
       public class LaborContractsService : ILaborContractService
       {
              private IRepository<Employee> empRepository;
              private IMapEntity mapper;
              //private IFactorySortCollection<LaborContractVM> addressesFactory;

              public LaborContractsService( IRepository<Employee> empRepository, IMapEntity mapper )
              {
                     this.empRepository = empRepository;

                     this.mapper = mapper;
              }

              public IQueryable<LaborContractVM>? All( int? companyId )
              {
                     IQueryable<EmploymentContract>? contracts =
                            this.empRepository
                            .AllAsNoTracking()
                            .Where( x => x.CompanyId == companyId
                                                   && x.EmploymentContract.HasBeenDeleted == false )
                            .Select<Employee, EmploymentContract>( x => x.EmploymentContract );

                     IQueryable<LaborContractVM>? empContractsVM =
                            this.mapper.ProjectTo<EmploymentContract, LaborContractVM>( contracts );

                     return empContractsVM;
              }

              public IQueryable<LaborContractVM>? AllActive( int? companyId )
              {
                     IQueryable<LaborContractVM>? empContractsVM = this.All( companyId )
                                                                                                                          .Where( x => x.IsActive == true );

                     return empContractsVM;
              }
       }
}


