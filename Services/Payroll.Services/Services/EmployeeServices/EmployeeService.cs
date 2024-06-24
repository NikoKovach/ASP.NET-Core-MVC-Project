using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Data.Common;
using Payroll.Mapper.CustomMap;
using Payroll.ModelsDto.EmployeeDtos;
using Payroll.Services.Services.ServiceContracts;
using System.Diagnostics;

namespace Payroll.Services.Services.EmployeeServices
{
     public class EmployeeService : AddUpdateEntity,IEmployee  
     {
          //private PayrollContext dbContext;
		private IGetEmployeeMapping customMapper;

          public EmployeeService( PayrollContext payrollContext, 
			IGetEmployeeMapping customMapper) : base(payrollContext)
          {
               EntityConfirmation.ArgumentNullConfirmation( payrollContext,
				nameof(payrollContext ),EntityConfirmation.GetClassName(this) ,
				EntityConfirmation.GetClassFullName( this));

			this.customMapper = customMapper;
          }

		public IQueryable<GetEmployeeDto> AllEmployees( int companyId )
		{
			IQueryable<GetEmployeeDto>? allEmpList = 
				this.customMapper.MapAllEmployeesQueryable(this.Context,companyId);

			return  allEmpList;
		}

		public IQueryable<GetEmployeeDto> AllPresentEmployees( int companyId )
		{
			IQueryable<GetEmployeeDto>? empList = 
				this.customMapper.MapPresentEmployeesQueryable(this.Context,companyId);

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


          