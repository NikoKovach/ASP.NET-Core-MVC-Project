﻿using System.Runtime.CompilerServices;
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
       public class CompanyService : ICompanyService
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
                     var companies = repository.AllAsNoTracking()
                                                                     .Where( x => x.HasBeenDeleted == false );

                     var companyList = mapEntity.ProjectTo<Company, SearchCompanyVM>( companies )
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

              public void CreateUpdateCompanyFolder( string appFolderPath, CompanyViewModel viewModel,
               [CallerMemberName] string actionName = "", params string[] viewModelOld )
              {
                     if ( actionName.Equals( "Create" ) )
                     {
                            CreateCompanyFolder( appFolderPath, viewModel );
                     }
                     else if ( actionName.Equals( "EditCompany" ) )
                     {
                            string oldCompanyName = viewModelOld[ 0 ];
                            string modifiedOldName =
                                   EnvironmentService.ModifyName( oldCompanyName );

                            if ( Directory.Exists( Path.Combine( appFolderPath, modifiedOldName ) ) )
                            {
                                   string modifiedNewName =
                                   EnvironmentService.ModifyName( viewModel.Name );

                                   RenameCompanyFolder( appFolderPath, modifiedOldName,
                                          modifiedNewName );
                            }
                            else
                            {
                                   CreateCompanyFolder( appFolderPath, viewModel );
                            }
                     }
              }

              //**************************************************************************
              private void CreateCompanyFolder( string appFolderPath, CompanyViewModel viewModel )
              {
                     string? companyName = this.repository.AllAsNoTracking()
                            .Where( x => x.UniqueIdentifier == viewModel.UniqueIdentifier )
                            .Select( x => x.Name )
                            .FirstOrDefault();

                     if ( companyName.Equals( viewModel.Name ) )
                     {
                            string modifiedCompanyName = EnvironmentService.ModifyName( viewModel.Name );

                            Directory.CreateDirectory( Path.Combine( appFolderPath, modifiedCompanyName ) );
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

//public async Task UpdateAsync( ICollection<CompanyViewModel> viewModels )
//{
//       List<Company>? companyList = mapEntity.Map<List<CompanyViewModel>, List<Company>>( viewModels.ToList() );

//       repository.Update( companyList );

//       await repository.SaveChangesAsync();
//}