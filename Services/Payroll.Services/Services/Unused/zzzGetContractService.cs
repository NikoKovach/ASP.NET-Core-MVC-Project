//using Microsoft.EntityFrameworkCore;
//using Payroll.Data;
//using Payroll.Models;
//using Payroll.Services.Services.ServiceContracts;
//using Payroll.ViewModels.EmployeeViewModels;

//namespace Payroll.Services.Services.EmployeeServices
//{
//       public class GetContractService
//       {
//              //private PayrollContext db;

//              //public GetContractService( PayrollContext context )
//              //{
//              //       this.db = context;
//              //}

//              //public async Task<ContractEmpVM> GetContractAsync( int? companyId, int empId )
//              //{
//              //       IQueryable<Employee> empDbSet = this.db.Set<Employee>()
//              //              .Where( x => x.CompanyId == companyId && x.Id == empId );

//              //       ContractEmpVM? contract = await empDbSet
//              //              .Select( x => new ContractEmpVM
//              //              {
//              //                     JobTitle = x.EmploymentContract.JobTitle,
//              //                     DepartmentName = x.EmploymentContract.Department.Name,
//              //                     ContractType = x.EmploymentContract.ContractType.Type,
//              //                     ContractNumber = x.EmploymentContract.ContractNumber,
//              //                     ContractDate = x.EmploymentContract.ContractDate.ToShortDateString(),
//              //                     AgreementsCount = x.EmploymentContract
//              //                                              .SupplementaryAgreements
//              //                                              .Count,
//              //              }
//              //              )
//              //              .FirstOrDefaultAsync();

//              //       if ( contract.AgreementsCount > 0 )
//              //       {
//              //              await GetInfoFromAnnexes( contract, empDbSet );
//              //       }

//              //       return contract;
//              //}

//              //private async Task GetInfoFromAnnexes( ContractEmpVM contract,
//              //                                             IQueryable<Employee> empDbSet )
//              //{
//              //       var annex = await empDbSet
//              //                            .Select( x => x.EmploymentContract
//              //                                              .SupplementaryAgreements
//              //                                              .Select( z => new
//              //                                              {
//              //                                                     z.Id,
//              //                                                     z.JobTitle,
//              //                                                     DepartmentName = z.Department.Name
//              //                                              } )
//              //                                              .OrderBy( x => x.Id )
//              //                                              .LastOrDefault() )
//              //                            .FirstOrDefaultAsync();

//              //       if ( !string.IsNullOrEmpty( annex.JobTitle ) )
//              //              contract.JobTitle = annex.JobTitle;

//              //       if ( !string.IsNullOrEmpty( annex.DepartmentName ) )
//              //              contract.DepartmentName = annex.DepartmentName;
//              //}
//       }
//}
