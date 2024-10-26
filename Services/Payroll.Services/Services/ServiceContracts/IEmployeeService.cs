﻿using Payroll.Data.Common;
using Payroll.Models;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IEmployeeService : IAddUpdate<EmployeeVM>, IGetEntity<EmployeeVM>
       {
              IRepository<Employee> Repository { get; }

              IQueryable<GetEmployeeVM> AllActive_GetEmployeeVM( int companyId );

              IQueryable<AllEmployeeVM> AllActive_AllEmployeeVM( int? companyId );

              IQueryable<GetEmployeeVM> AllEmployees( int companyId );

              Task<bool> CreateEmployeeFolderAsync( string rootFolder, int PersonId, int CompanyId );

              Task<string?> UploadEmployeePictureAsync( EmployeeVM viewModel, string? employeeFolder );

              Task UpdatePersonAsync( int personId, string? employeeFolder, string? relativeFolder, string? appFolder );

              //Task<int?> DeleteEmployee( int? employeeId );
       }
}
