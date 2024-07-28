using Payroll.Data.Common;
using Payroll.Mapper.CustomMap;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Services.Services.EmployeeServices
{
     public class EmployeeService :IEmployee  
     {
		private IGetEmployeeMapping customMapper;
		private IRepository<Employee> repository;

          public EmployeeService( IRepository<Employee> empRepository, 
			IGetEmployeeMapping customMapper)
          {
               EntityConfirmation.ArgumentNullConfirmation( empRepository,
				nameof(empRepository ),EntityConfirmation.GetClassName(this) ,
				EntityConfirmation.GetClassFullName( this));

			EntityConfirmation.ArgumentNullConfirmation( customMapper,
				nameof(customMapper ),EntityConfirmation.GetClassName(this) ,
				EntityConfirmation.GetClassFullName( this));

			this.repository = empRepository;

			this.customMapper = customMapper;
          }

		public IQueryable<GetEmployeeVM> AllEmployees( int companyId )
		{
			IQueryable<GetEmployeeVM>? allEmpList = 
				this.customMapper.MapAllEmployeesQueryable
				(this.repository.DbSet,companyId);

			return  allEmpList;
		}

		public IQueryable<GetEmployeeVM> AllPresentEmployees( int companyId )
		{
			IQueryable<GetEmployeeVM>? empList = 
				this.customMapper.MapPresentEmployeesQueryable
				(this.repository.DbSet,companyId);

			return  empList;
		}

		public Task AddAsync( EmployeeVM viewModel )
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync( EmployeeVM viewModel )
		{
			throw new NotImplementedException();
		}

		public async Task SaveAsync( )
		{
			await this.repository.SaveChangesAsync();
		}
	}
}
