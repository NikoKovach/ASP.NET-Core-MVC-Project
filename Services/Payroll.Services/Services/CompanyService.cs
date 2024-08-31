using System.Runtime.CompilerServices;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.Services.Utilities;
using Payroll.ViewModels;

namespace Payroll.Services.Services
{
       /// <summary>
       /// TODO : FOR TESTING
       /// </summary>
       public class CompanyService : ICompany
       {
              private IMapEntity mapEntity;
              private IRepository<Company> repository;

              public CompanyService( IRepository<Company> repository, IMapEntity customMapper )
              {
                     mapEntity = customMapper;

                     this.repository = repository;
              }

              public IQueryable<CompanyViewModel> All()
              {
                     var companiesList = mapEntity
                                                                .ProjectTo<Company, CompanyViewModel>( repository.AllAsNoTracking() )
                                                                .OrderBy( c => c.Name )
                                                                .ThenBy( c => c.Id );

                     return companiesList;
              }

              public IQueryable<CompanyViewModel> AllActive()
              {
                     var companiesList = mapEntity
                                                                .ProjectTo<Company, CompanyViewModel>( repository.AllAsNoTracking() )
                                                                .Where( x => x.HasBeenDeleted == false )
                                                                .OrderBy( c => c.Name )
                                                                .ThenBy( c => c.Id );

                     return companiesList;
              }

              public IQueryable<CompanyViewModel> AllActive( string companyId )
              {
                     var companies = repository
                                                          .AllAsNoTracking()
                                                          .Where( x => x.HasBeenDeleted == false &&
                                                                                  x.UniqueIdentifier.Equals( companyId ) );

                     IQueryable<CompanyViewModel>? company = mapEntity
                            .ProjectTo<Company, CompanyViewModel>( companies );

                     return company;
              }

              public IQueryable<SearchCompanyVM> AllActive_SearchCompanyVM()
              {
                     var companies = repository
                                                          .AllAsNoTracking()
                                                          .Where( x => x.HasBeenDeleted == false );

                     var companyList = mapEntity
                                                             .ProjectTo<Company, SearchCompanyVM>( companies )
                                                             .OrderBy( c => c.Name );

                     return companyList;
              }

              public async Task AddAsync( CompanyViewModel viewModel )
              {
                     var company = mapEntity.Map<CompanyViewModel, Company>( viewModel );

                     await repository.AddAsync( company );

                     await repository.SaveChangesAsync();
              }

              public async Task UpdateAsync( CompanyViewModel viewModel )
              {
                     var company = mapEntity.Map<CompanyViewModel, Company>( viewModel );

                     repository.Update( company );

                     await repository.SaveChangesAsync();
              }

              public void CreateUpdateCompanyFolder( string rootFolder, CompanyViewModel viewModel,
               [CallerMemberName] string actionName = "", params string[] viewModelOld )
              {
                     if ( actionName.Equals( "Create" ) )
                     {
                            CreateCompanyFolder( rootFolder, viewModel );
                     }
                     else if ( actionName.Equals( "EditCompany" ) )
                     {
                            string oldCompanyName = viewModelOld[ 0 ];
                            string modifiedOldName =
                                   EnvironmentService.ModifyFolderName( oldCompanyName );

                            if ( EnvironmentService.DirectoryExists( rootFolder, modifiedOldName ) )
                            {
                                   string modifiedNewName =
                                   EnvironmentService.ModifyFolderName( viewModel.Name );

                                   RenameCompanyFolder( rootFolder, modifiedOldName,
                                          modifiedNewName );
                            }
                            else
                            {
                                   CreateCompanyFolder( rootFolder, viewModel );
                            }
                     }
              }

              //**************************************************************************
              private void CreateCompanyFolder( string rootFolder, CompanyViewModel viewModel )
              {
                     CompanyViewModel? existedCompany = AllActive( viewModel.UniqueIdentifier )
                                                                                                      .FirstOrDefault();

                     if ( existedCompany != null )
                     {
                            EnvironmentService.CreateFolder( rootFolder, viewModel.Name );
                     }
              }

              private void RenameCompanyFolder( string rootFolder, string modifiedOldName,
                                                                                                                string modifiedNewName )
              {
                     string sourceDirectory = Path.Combine( rootFolder, modifiedOldName );
                     //@"C:\zzz-source";

                     string destinationDirectory = Path.Combine( rootFolder, modifiedNewName );
                     //@"C:\zzz-destination";

                     try
                     {
                            Directory.Move( sourceDirectory, destinationDirectory );
                     }
                     catch ( Exception )
                     {
                            throw new InvalidOperationException();
                     }
              }
       }
}