using CommonServices;
using Microsoft.EntityFrameworkCore;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.ModelsDto;

namespace Payroll.Services.Services.CompanyServices
{
	/// <summary>
     /// TODO : FOR TESTING
     /// </summary>
     public class CompanyService : ICompany,IGetCompany
     {
		private IMapEntity mapEntity;
		private IRepository<Company> repository;
		public CompanyService( IRepository<Company> repository,IMapEntity customMapper ) 
          {
			EntityConfirmation.ArgumentNullConfirmation
							( customMapper, nameof( customMapper ),
							  EntityConfirmation.GetClassName( this ),
							  EntityConfirmation.GetClassFullName( this )
							);

			mapEntity = customMapper;

			this.repository = repository;
          }

          public virtual async Task<ICollection<CompanyDto>> GetAllCompaniesAsync()
          {
			var companiesList = await this.mapEntity
							.ProjectTo<Company, CompanyDto>(this.repository.DbSet)
							.OrderBy( c => c.Name )
							.ThenBy( c => c.Id )
							.ToListAsync();

			return companiesList;
          }

		public virtual async Task<ICollection<CompanyDto>> GetAllValidCompaniesAsync()
          {
			var companiesList = await this.mapEntity
						.ProjectTo<Company, CompanyDto>(this.repository.DbSet)
						.Where( x => x.HasBeenDeleted == false )
						.OrderBy( c => c.Name )
						.ThenBy( c => c.Id )
						.ToListAsync();
			
               return companiesList;
          }

		public async Task<CompanyDto> GetActiveCompanyByUniqueIdAsync
			( string companyUniqueId )
          {
			CompanyDto? company = await this.mapEntity
				.ProjectTo<Company,CompanyDto>(this.repository.DbSet)
				.Where( x => x.HasBeenDeleted == false 
                                    && x.UniqueIdentifier.Equals( companyUniqueId) )
				.FirstOrDefaultAsync();

               return company;
          }

		public async Task AddAsync( CompanyDto viewModel )
		{
			var company = this.mapEntity.Map<CompanyDto,Company>(viewModel);

			await this.repository.AddAsync( company );

			await this.repository.SaveChangesAsync();
		}

		public async Task UpdateAsync(CompanyDto viewModel)
		{
			var company = this.mapEntity.Map<CompanyDto,Company>(viewModel);

			this.repository.Update( company );

			await this.repository.SaveChangesAsync();
		}

		public void CreateUpdateCompanyFolder(string rootFolder, 
			CompanyDto viewModel,string actionName,params string[] viewModelOld)
		{
			if ( actionName.Equals("Create") )
			{
				CreateCompanyFolder( rootFolder, viewModel );
			}
			else if ( actionName.Equals("EditCompany") )
			{
				string oldCompanyName = viewModelOld[ 0 ];
				string modifiedOldName =
					EnvironmentService.ModifyFolderName( oldCompanyName );

				if ( EnvironmentService.DirectoryExists(rootFolder,modifiedOldName) )
				{
					string modifiedNewName =
					EnvironmentService.ModifyFolderName( viewModel.Name );

					RenameCompanyFolder(rootFolder,modifiedOldName,
						modifiedNewName);
				}
				else
				{
					CreateCompanyFolder( rootFolder, viewModel );
				}
			}
		}

		private void CreateCompanyFolder(string rootFolder, 
			CompanyDto viewModel)
		{ 
			CompanyDto? existedCompany = this.GetActiveCompanyByUniqueIdAsync
									   ( viewModel.UniqueIdentifier )
									   .GetAwaiter()
									   .GetResult();

			if (existedCompany != null  )
			{
				EnvironmentService.CreateFolder( rootFolder, viewModel.Name );
			}
		}

		private void RenameCompanyFolder( string rootFolder, 
			string modifiedOldName, string modifiedNewName )
		{
			string sourceDirectory = Path.Combine(rootFolder,modifiedOldName);
			//@"C:\zzz-source";

			string destinationDirectory = Path.Combine(rootFolder,modifiedNewName);
			//@"C:\zzz-destination";

			try
			{
			    Directory.Move(sourceDirectory, destinationDirectory);
			}
			catch (Exception)
			{
			    throw new InvalidOperationException();
			}
		}
	}
}


//CompanyDto? existedCompany = await this.service
				//						 .GetActiveCompanyByUniqueIdAsync
				//						 ( modelDto.UniqueIdentifier );

				//if (existedCompany != null  )
				//{
				//	string appFolderPath = Path.Combine( this.env.ContentRootPath,
				//			   this.config[ "PrimaryAppFolder" ] );


				//	EnvironmentService.CreateFolder
				//					( appFolderPath, modelDto.Name );
				//}