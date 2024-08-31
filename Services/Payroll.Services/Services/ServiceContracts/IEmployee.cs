using Payroll.Data.Common;
using Payroll.Models;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IEmployee : IAddUpdate<EmployeeVM>, IGetEntity<EmployeeVM>
       {
              IRepository<Employee> Repository { get; }

              IQueryable<GetEmployeeVM> AllActive_GetEmployeeVM( int companyId );

              IQueryable<AllEmployeeVM> AllActive_AllEmployeeVM( int companyId );

              IQueryable<GetEmployeeVM> AllEmployees( int companyId );

              Task<string> CreateEmployeeFolderAsync( string rootFolder, int PersonId, int CompanyId );

              string CreateFileNameWithPath( string employeeFolder, string oldFileName );
       }
}
