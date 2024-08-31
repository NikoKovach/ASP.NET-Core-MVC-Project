using Payroll.Models;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Mapper.CustomMap.Test
{
       public class AllEmployeeProfile
       {

              public IQueryable<AllEmployeeVM>? CreateProjection( IQueryable<Employee> employees )
              {
                     IQueryable<AllEmployeeVM>? result = employees.Select( x => new AllEmployeeVM
                     {
                            Id = x.Id,
                            NumberFromTheList = x.NumberFromTheList,
                            FullName = $"{x.Person.FirstName} {x.Person.MiddleName} {x.Person.LastName}",
                            EGN = x.Person.EGN,
                            //DepartmentName = x.EmploymentContract.Department.Name,
                            //JobTitle = x.EmploymentContract.JobTitle,
                            //ContractNumber = x.EmploymentContract.ContractNumber,
                            //ContractDate = x.EmploymentContract.ContractDate.ToShortDateString()
                     } );

                     return result;
              }
       }
}
