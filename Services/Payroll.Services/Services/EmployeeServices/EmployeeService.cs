
using Microsoft.EntityFrameworkCore;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Mapper.CustomMap;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.Services.Utilities;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Services.Services.EmployeeServices
{

       public class EmployeeService : IEmployee
       {
              private IMapEntity mapper;
              private ICustomProjections customProjections;
              private IRepository<Employee> repository;

              public EmployeeService( IRepository<Employee> empRepository, IMapEntity mapper,
                     ICustomProjections projections )
              {
                     this.repository = empRepository;

                     this.mapper = mapper;

                     this.customProjections = projections;
              }

              public IRepository<Employee> Repository { get => this.repository; }

              public IQueryable<GetEmployeeVM> AllEmployees( int companyId )
              {
                     IQueryable<Employee>? employees = this.repository
                                                                                          .AllAsNoTracking()
                                                                                          .Where( x => x.CompanyId == companyId );

                     IQueryable<GetEmployeeVM>? allEmpList
                            = (IQueryable<GetEmployeeVM>) this.customProjections
                               .EmployeeProjections[ "GetEmployeeVM" ]( employees );

                     return allEmpList;
              }

              public IQueryable<GetEmployeeVM> AllActive_GetEmployeeVM( int companyId )
              {
                     IQueryable<Employee>? employees = AllActiveEntities( companyId );

                     IQueryable<GetEmployeeVM>? result =
                            (IQueryable<GetEmployeeVM>) this.customProjections
                                                                                             .EmployeeProjections[ "GetEmployeeVM" ]( employees );

                     return result;
              }

              public IQueryable<AllEmployeeVM> AllActive_AllEmployeeVM( int companyId )
              {
                     IQueryable<Employee>? employees = AllActiveEntities( companyId );

                     IQueryable<AllEmployeeVM>? result =
                            (IQueryable<AllEmployeeVM>) this.customProjections
                                                                                           .EmployeeProjections[ "AllEmployees" ]( employees );

                     return result;
              }

              public async Task<EmployeeVM?> GetEntityAsync( int? entityId )
              {
                     Employee? employee = await this.repository
                                                                                .AllAsNoTracking()
                                                                                .Where( x => x.IsPresent == true )
                                                                                .FirstOrDefaultAsync( x => x.Id == entityId );

                     var empViewModel = this.mapper.Map<Employee, EmployeeVM>( employee );

                     return empViewModel;
              }

              //#############################################################
              public async Task AddAsync( EmployeeVM viewModel )
              {
                     Employee? employee = this.mapper.Map<EmployeeVM, Employee>( viewModel );

                     await this.repository.AddAsync( employee );

                     await this.repository.SaveChangesAsync();
              }

              public async Task UpdateAsync( EmployeeVM viewModel )
              {
                     Employee employee = this.mapper.Map<EmployeeVM, Employee>( viewModel );

                     this.repository.Update( employee );

                     await this.repository.SaveChangesAsync();
              }

              public async Task SaveAsync()
              {
                     await this.repository.SaveChangesAsync();
              }

              public async Task<string> CreateEmployeeFolderAsync( string rootFolder, int personId, int companyId )
              {
                     string? companyName = await this.repository.AllAsNoTracking()
                                                                                           .Where( x => x.CompanyId == companyId )
                                                                                           .Select( x => x.Company.Name )
                                                                                           .FirstOrDefaultAsync();

                     string? employeeName = await this.repository.AllAsNoTracking()
                                                                                       .Where( x => x.CompanyId == companyId &&
                                                                                       x.PersonId == personId )
                                                                                       .Select( x => x.Person.FullName )
                                                                                       .FirstOrDefaultAsync();

                     //string? employeeName = "Dida B Koleva";
                     string modifiedCompanyName = EnvironmentService.ModifyFolderName( companyName );

                     string modifiedEmpName = EnvironmentService.ModifyFolderName( employeeName );

                     bool companyDirExists = EnvironmentService.DirectoryExists( rootFolder, modifiedCompanyName );

                     if ( companyDirExists )
                     {
                            return CreateEmployeeFolder( rootFolder, modifiedCompanyName, modifiedEmpName );
                     }
                     else
                     {
                            EnvironmentService.CreateFolder( rootFolder, companyName );

                            return CreateEmployeeFolder( rootFolder, modifiedCompanyName, modifiedEmpName );
                     }
              }

              public string CreateFileNameWithPath( string employeeFolder, string oldFileName )
              {
                     string employeeName = GetEmlpoyeeName( employeeFolder );

                     FileInfo fileInfo = new FileInfo( oldFileName );

                     string fileName = $"{employeeName}-profile-img{fileInfo.Extension}";

                     string fileNameWithPath = Path.Combine( employeeFolder, fileName );

                     return fileNameWithPath;
              }

              //**************************************************************************

              private IQueryable<Employee>? AllActiveEntities( int companyId )
              {
                     IQueryable<Employee>? employees = this.repository
                                               .AllAsNoTracking()
                                               .Where( x => x.CompanyId == companyId && x.IsPresent == true );

                     return employees;
              }


              private string CreateEmployeeFolder( string rootFolder, string companyName,
                                                                                    string employeeName )
              {
                     string employeesFolder = EnvironmentService
                                   .NewFolderPath( rootFolder, companyName ) + @"\Employees";

                     Directory.CreateDirectory( EnvironmentService
                                                                      .NewFolderPath( employeesFolder, employeeName ) );

                     string path = $@"{employeesFolder}\{employeeName}";

                     return $@"{employeesFolder}\{employeeName}";
              }

              private string GetEmlpoyeeName( string employeeFolder )
              {
                     var reverseSlash = char.Parse( @"\" );

                     int reverseSlashIndex = employeeFolder.LastIndexOf( reverseSlash );

                     string? profileEmpName = employeeFolder.Substring( reverseSlashIndex + 1 );

                     return profileEmpName;
              }
       }
}
