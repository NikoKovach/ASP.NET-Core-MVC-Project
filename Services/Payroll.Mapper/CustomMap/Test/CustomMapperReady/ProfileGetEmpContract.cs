using Payroll.Models;

namespace Payroll.Mapper.CustomMap.Test.CustomMapperReady
{
       public class ProfileGetEmpContract : IProjectionTest
       {
              public IQueryable<EmployeeBaseView> ProjectionTest( IQueryable<Employee> employees )
              {
                     var contract = employees
                            .Select( x => new ContractView
                            {
                                   Id = x.Id,
                                   CompanyId = x.CompanyId,
                                   NumberFromTheList = x.NumberFromTheList,
                                   IsPresent = x.IsPresent,
                                   Contract = new ContractSubView
                                   {
                                          JobTitle = x.EmploymentContract.JobTitle,
                                          DepartmentName = x.EmploymentContract.Department.Name,
                                          ContractType = x.EmploymentContract.ContractType.Type,
                                          ContractNumber = x.EmploymentContract.ContractNumber,
                                          ContractDate = x.EmploymentContract.ContractDate.Date,
                                          //LastAgreement = x.EmploymentContract.SupplementaryAgreements
                                          //                            .SelectMany(x =>x.Id )
                                   }
                            } );

                     return contract;
              }
       }
}
