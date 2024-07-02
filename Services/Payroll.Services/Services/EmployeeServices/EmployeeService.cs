using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Data.Common;
using Payroll.Mapper.CustomMap;
using Payroll.Models;
using Payroll.ModelsDto.EmployeeDtos;
using Payroll.Services.Services.ServiceContracts;
using System.Diagnostics;

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

		public IQueryable<GetEmployeeDto> AllEmployees( int companyId )
		{
			IQueryable<GetEmployeeDto>? allEmpList = 
				this.customMapper.MapAllEmployeesQueryable
				(this.repository.DbSet,companyId);

			return  allEmpList;
		}

		public IQueryable<GetEmployeeDto> AllPresentEmployees( int companyId )
		{
			IQueryable<GetEmployeeDto>? empList = 
				this.customMapper.MapPresentEmployeesQueryable
				(this.repository.DbSet,companyId);

			return  empList;
		}

		public Task AddAsync( EmployeeDto viewModel )
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync( EmployeeDto viewModel )
		{
			throw new NotImplementedException();
		}

		private async Task GetContractInfoFromAnnexesAsync
			( PayrollContext dbContext,IList<GetEmployeeDto> empList )
		{
			foreach ( var employee in empList )
			{
				var annexes = await dbContext.Employees
					.Where( x => x.Id == employee.Id )
					.Select(x => x.EmploymentContract
							    .SupplementaryAgreements
							    .OrderBy(s => s.Id)
							    .ToList())
					.FirstOrDefaultAsync();

				if ( annexes != null && annexes.Any() )
				{
					employee.ContractInfo.JobTitle 
									= annexes.Last().JobTitle;
					employee.ContractInfo.DepartmentName = await 
						dbContext
					     .Departments
					     .Where(x => x.DepartmentID == annexes.Last().DepartmentId)
					     .Select(x => x.Name)
					     .FirstOrDefaultAsync();
					employee.ContractInfo.ContractType = await 
						dbContext
						.ContractTypes
						.Where(x => x.Id == annexes.Last().ContractTypeId)
						.Select(x => x.Type)
						.FirstOrDefaultAsync();
					employee.ContractInfo.ContractNumber 
									= annexes.Last().AgreementNumber;
					employee.ContractInfo.ContractDate	
									= annexes.Last().DateOfAgreementOrChange;
				}
			}
		}
	}
}


          