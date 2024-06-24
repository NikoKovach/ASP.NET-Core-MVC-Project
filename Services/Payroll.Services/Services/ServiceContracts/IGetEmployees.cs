
using Payroll.ModelsDto.EmployeeDtos;

namespace Payroll.Services.Services.ServiceContracts
{
	public interface IGetEmployees
	{		
		IQueryable<GetEmployeeDto> AllPresentEmployees( int companyId );

		IQueryable<GetEmployeeDto> AllEmployees( int companyId );
	}
}
