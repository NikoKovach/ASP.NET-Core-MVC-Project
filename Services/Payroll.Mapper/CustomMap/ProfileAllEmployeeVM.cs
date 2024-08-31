using Payroll.Models;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Mapper.CustomMap
{
       public class ProfileAllEmployeeVM : IProjection
       {
              public IQueryable<BaseEmployeeVM> Projection( IQueryable<Employee> employees )
              {
                     IQueryable<AllEmployeeVM>? empView = employees
                                                  .Select( x => new AllEmployeeVM
                                                  {
                                                         Id = x.Id,
                                                         CompanyId = x.CompanyId,
                                                         NumberFromTheList = x.NumberFromTheList,
                                                         FullName = x.Person.FullName,
                                                         EGN = x.Person.EGN,
                                                         ContractInfo = new ContractEmpVM
                                                         {
                                                                JobTitle = x.EmploymentContract.JobTitle,
                                                                DepartmentName = x.EmploymentContract.Department.Name,
                                                                ContractType = x.EmploymentContract.ContractType.Type,
                                                                ContractNumber = x.EmploymentContract.ContractNumber,
                                                                ContractDate = x.EmploymentContract.ContractDate.ToShortDateString(),
                                                                LastAnnex = x.EmploymentContract.SupplementaryAgreements
                                                                                                            .Select( a => new AnnexJobTitleVM
                                                                                                            {
                                                                                                                   Id = a.Id,
                                                                                                                   JobTitle = a.JobTitle,
                                                                                                                   DepartmentName = a.Department.Name

                                                                                                            } )
                                                                                                              .OrderBy( x => x.Id )
                                                                                                              .LastOrDefault()
                                                         },
                                                  } );

                     return empView;
              }
       }
}

/*
ContractInfo = new ContractEmpVM
                                                         {
                                                                JobTitle = x.EmploymentContract.JobTitle,
                                                                DepartmentName = x.EmploymentContract.Department.Name,
                                                                ContractType = x.EmploymentContract.ContractType.Type,
                                                                ContractNumber = x.EmploymentContract.ContractNumber,
                                                                ContractDate = x.EmploymentContract.ContractDate.ToShortDateString(),
                                                                LastAnnex = x.EmploymentContract.SupplementaryAgreements
                                                                                                            .Select( a => new AnnexJobTitleVM
                                                                                                            {
                                                                                                                   Id = a.Id,
                                                                                                                   JobTitle = a.JobTitle,
                                                                                                                   DepartmentName = a.Department.Name

                                                                                                            } )
                                                                                                              .OrderBy( x => x.Id )
                                                                                                              .LastOrDefault()
                                                         },


*/