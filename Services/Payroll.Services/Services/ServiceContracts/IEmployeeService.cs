using Payroll.Data.Common;
using Payroll.Models;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IEmployeeService : IBasicAddUpdate<EmployeeVM>, IBasicGetEntityIQueryable<EmployeeVM>
       {
              IRepository<Employee> Repository { get; }

              IQueryable<GetEmployeeVM> AllActive_GetEmployeeVM( int companyId );

              IQueryable<AllEmployeeVM> AllActive_AllEmployeeVM( int? companyId );

              IQueryable<SearchEmployeeVM> AllActive_SearchEmployeeVM( int? companyId );

              IQueryable<GetEmployeeVM> AllEmployees( int companyId );

              Task<bool> CreateEmployeeFolderAsync( string rootFolder, int PersonId, int CompanyId );

              Task<string?> UploadEmployeePictureAsync( EmployeeVM viewModel, string? employeeFolder );

              Task UpdatePersonAsync( int personId, string? employeeFolder, string? relativeFolder, string? appFolder );

              Task<string?> GetEmployeeName( int? employeeId );

       }
}
