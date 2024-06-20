using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Mapper.CustomMap;
using Payroll.ModelsDto.EmployeeDtos;
using Payroll.Services.Services.ServiceContracts;
using System.Diagnostics;
using static Payroll.Services.AuthenticServices.EntityConfirmation;

namespace Payroll.Services.Services.EmployeeServices
{
     public class EmployeeService : IEmployeeService   
     {
          /// <summary>
          /// TODO : FOR TESTING
          /// </summary>
          /// 

          private PayrollContext dbContext;
		private IGetEmployeeMapping customMapper;

          public EmployeeService( PayrollContext payrollContext, 
			IGetEmployeeMapping customMapper)
          {
               ArgumentNullConfirmation( payrollContext,nameof(payrollContext ),
                                         GetClassName(this) ,GetClassFullName( this));

               this.dbContext = payrollContext;

			this.customMapper = customMapper;
          }

		public async Task<IList<GetEmployeeDto>> GetAllEmployeesAsync
		( int companyId ) 
		{
			//Stopwatch stopWatch = new Stopwatch();
			//stopWatch.Start();

			IList<GetEmployeeDto> empList = await this.customMapper
				.MapAllEmployeesAsync(this.dbContext,companyId);

			//Console.WriteLine(stopWatch.ElapsedMilliseconds);

			await GetContractInfoFromAnnexesAsync( dbContext, empList );

			//Console.WriteLine(stopWatch.ElapsedMilliseconds);
			//stopWatch.Stop();
			return empList;
		}

		public async Task<IList<GetEmployeeDto>> GetAllPresentEmployeesAsync( int companyId )
		{
			IList<GetEmployeeDto> empList = await this.customMapper
				.MapPresentEmployeesAsync(this.dbContext,companyId);

			await GetContractInfoFromAnnexesAsync(dbContext,empList);

			return empList;
		}

		public IQueryable<GetEmployeeDto> AllPresentEmployees( int companyId )
		{
			IQueryable<GetEmployeeDto>? empList = this.customMapper.MapPresentEmployeesQueryable(dbContext,companyId);

			return  empList;
		}

		private async Task GetContractInfoFromAnnexesAsync( PayrollContext dbContext,
											IList<GetEmployeeDto> empList )
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


          