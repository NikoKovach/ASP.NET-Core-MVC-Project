using Microsoft.EntityFrameworkCore;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Mapper.CustomMap;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.Services.Utilities;
using Payroll.Services.UtilitiesServices.Messages;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Services.Services
{
       public class EmployeeService : IEmployeeService
       {
              private IMapEntity mapper;
              private ICustomProjections customProjections;
              private IRepository<Employee> repository;

              public EmployeeService( IRepository<Employee> empRepository, IMapEntity mapper,
                     ICustomProjections projections )
              {
                     repository = empRepository;

                     this.mapper = mapper;

                     customProjections = projections;
              }

              public IRepository<Employee> Repository { get => repository; }

              public IQueryable<GetEmployeeVM> AllEmployees( int companyId )
              {
                     IQueryable<Employee>? employees = repository
                                                                                          .AllAsNoTracking()
                                                                                          .Where( x => x.CompanyId == companyId );

                     IQueryable<GetEmployeeVM>? allEmpList
                            = (IQueryable<GetEmployeeVM>) customProjections
                               .EmployeeProjections[ "GetEmployeeVM" ]( employees );

                     return allEmpList;
              }

              public IQueryable<GetEmployeeVM> AllActive_GetEmployeeVM( int companyId )
              {
                     IQueryable<Employee>? employees = AllActiveEntities( companyId );

                     IQueryable<GetEmployeeVM>? result =
                            (IQueryable<GetEmployeeVM>) customProjections
                            .EmployeeProjections[ "GetEmployeeVM" ]( employees );

                     return result;
              }

              public IQueryable<AllEmployeeVM> AllActive_AllEmployeeVM( int? companyId )
              {
                     IQueryable<Employee>? employees = this.AllActiveEntities( companyId );

                     IQueryable<AllEmployeeVM>? result =
                            (IQueryable<AllEmployeeVM>) customProjections.EmployeeProjections[ "AllEmployees" ]( employees );

                     return result;
              }

              public IQueryable<SearchEmployeeVM> AllActive_SearchEmployeeVM( int? companyId )
              {
                     IQueryable<Employee>? employees = repository
                                                                                         .AllAsNoTracking()
                                                                                         .Where( x => x.CompanyId == companyId );

                     IQueryable<SearchEmployeeVM>? empResultList =
                            this.mapper.ProjectTo<Employee, SearchEmployeeVM>( employees );

                     return empResultList;
              }

              public IQueryable<EmployeeVM>? GetEntity( int? entityId )
              {
                     IQueryable<Employee>? employee = repository
                                                                                       .AllAsNoTracking()
                                                                                       .Where( x => x.IsPresent == true && x.Id == entityId );

                     IQueryable<EmployeeVM>? empViewModel = mapper.ProjectTo<Employee, EmployeeVM>( employee );

                     return empViewModel;
              }

              public async Task<string?> GetEmployeeName( int? employeeId )
              {
                     string? name = await this.repository.AllAsNoTracking()
                                                                    .Where( x => x.Id == employeeId && x.IsPresent == true )
                                                                    .Select( x => x.Person.FullName )
                                                                    .FirstOrDefaultAsync();

                     return name;
              }

              //#############################################################
              public async Task AddAsync( EmployeeVM viewModel )
              {
                     Employee? employee = mapper.Map<EmployeeVM, Employee>( viewModel );

                     await repository.AddAsync( employee );

                     await repository.SaveChangesAsync();
              }

              public async Task UpdateAsync( EmployeeVM viewModel )
              {
                     Employee employee = mapper.Map<EmployeeVM, Employee>( viewModel );

                     repository.Update( employee );

                     await repository.SaveChangesAsync();
              }

              public async Task<bool> CreateEmployeeFolderAsync( string appFolder,
                     int personId, int companyId )
              {
                     string? employeeFolderPath = await GetEmployeeDirectoryAsync( appFolder, personId, companyId );

                     if ( string.IsNullOrEmpty( employeeFolderPath ) )
                            return false;

                     if ( Directory.Exists( employeeFolderPath ) )
                            return true;

                     string? companyFolderName = await GetCompanyFolderAsync( companyId );

                     if ( string.IsNullOrEmpty( companyFolderName ) )
                            return false;

                     if ( EnvironmentService.DirExists( appFolder, companyFolderName ) )
                     {
                            Directory.CreateDirectory( employeeFolderPath );

                            return true;
                     }

                     Directory.CreateDirectory( Path.Combine( appFolder, companyFolderName ) );

                     if ( EnvironmentService.DirExists( appFolder, companyFolderName ) )
                            Directory.CreateDirectory( employeeFolderPath );

                     return true;
              }

              public async Task<string?> UploadEmployeePictureAsync( EmployeeVM viewModel, string? appFolderPath )
              {
                     var imageFile = viewModel.ProfileImage;

                     if ( string.IsNullOrEmpty( appFolderPath ) || imageFile == null ||
                            imageFile.Length < OutputMessages.MinImageSizeInBytes )
                            return null;

                     string? empFolderPath = await GetEmployeeDirectoryAsync( appFolderPath,
                                                                        viewModel.PersonId, viewModel.CompanyId );

                     if ( string.IsNullOrEmpty( empFolderPath ) )
                            return null;

                     string? empFolderName = await GetEmpFolderNameAsync( viewModel.CompanyId,
                                                                                                                                     viewModel.PersonId );

                     FileInfo fileInfo = new FileInfo( imageFile.FileName );

                     string currentDate = DateTime.UtcNow.ToString( "yyyy-MM-dd_HHmmss" );
                     string fileName = $"{empFolderName}-profile-{currentDate}{fileInfo.Extension}";

                     string empImageFullPath = Path.Combine( empFolderPath, fileName );

                     try
                     {
                            FileStream? stream = new FileStream( empImageFullPath, FileMode.Create );

                            await imageFile.CopyToAsync( stream );

                            return empImageFullPath;
                     }
                     catch ( Exception )
                     {
                            return null;
                     }
              }

              public async Task UpdatePersonAsync( int personId, string? empImageFullPath,
                                                                                    string? relativeFolderName, string? appFolderName )
              {
                     if ( string.IsNullOrEmpty( empImageFullPath ) && string.IsNullOrEmpty( relativeFolderName )
                                                                        && string.IsNullOrEmpty( appFolderName ) && personId > 0 )
                            return;

                     string relativePath = EnvironmentService.CreateRelativePath( empImageFullPath,
                                                                                                   relativeFolderName, appFolderName );
                     //relativeFolderName, appFolderName
                     //#######################################################################
                     Employee? employee = await repository
                                                                                 .All()
                                                                                 .Where( x => x.Person.Id == personId )
                                                                                 .Include( i => i.Person )
                                                                                 .FirstOrDefaultAsync();

                     Repository.DbSet.Entry( employee ).State = EntityState.Detached;

                     employee.Person.PhotoFilePath = relativePath;

                     repository.Update( employee );

                     await repository.SaveChangesAsync();
              }

              public async Task DeleteAsync( int? employeeId, int? companyId = null )

              {
                     if ( employeeId == null || companyId == null )
                            await Task.CompletedTask;

                     Employee? employee = await repository
                                                                             .All()
                                                                             .Where( x => x.CompanyId == companyId && x.Id == employeeId )
                                                                             .FirstOrDefaultAsync();
                     if ( employee == null )
                            await Task.CompletedTask;

                     employee.IsPresent = false;

                     EntityState personState = this.repository.Context.Entry( employee ).State;

                     if ( personState == EntityState.Modified )
                            await this.repository.SaveChangesAsync();
              }

              //####################################################################

              private IQueryable<Employee>? AllActiveEntities( int? companyId )
              {
                     IQueryable<Employee>? employees = repository
                                               .AllAsNoTracking()
                                               .Where( x => x.CompanyId == companyId && x.IsPresent == true );

                     return employees;
              }

              private async Task<string?> GetEmpFolderNameAsync( int companyId, int personId )
              {
                     string? employeeName = await repository.AllAsNoTracking()
                                                                                       .Where( x => x.CompanyId == companyId &&
                                                                                       x.PersonId == personId )
                                                                                       .Select( x => x.Person.FullName )
                                                                                       .FirstOrDefaultAsync();

                     string modifiedEmpName = EnvironmentService.ModifyName( employeeName );

                     return modifiedEmpName;
              }

              private async Task<string?> GetCompanyFolderAsync( int companyId )
              {
                     string? companyName = await repository.AllAsNoTracking()
                                                                                          .Where( x => x.CompanyId == companyId )
                                                                                          .Select( x => x.Company.Name )
                                                                                          .FirstOrDefaultAsync();

                     string modifiedCompanyName = EnvironmentService.ModifyName( companyName );

                     return modifiedCompanyName;
              }

              private async Task<string?> GetEmployeeDirectoryAsync( string appFolder, int personId, int companyId )
              {
                     string? companyFolderName = await GetCompanyFolderAsync( companyId );

                     string? empFolderName = await GetEmpFolderNameAsync( companyId, personId );

                     if ( string.IsNullOrEmpty( empFolderName ) )
                            return null;

                     string empFolderPath = $@"{appFolder}\{companyFolderName}\Employees\{empFolderName}";

                     return empFolderPath;
              }
       }
}
