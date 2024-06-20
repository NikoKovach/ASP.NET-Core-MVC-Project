
using Payroll.ModelsDto.EmployeeDtos;

namespace Payroll.Services.Services.ServiceContracts
{
	public interface IGetEmployees
	{
		Task<IList<GetEmployeeDto>> GetAllEmployeesAsync(int companyId) ;

          Task<IList<GetEmployeeDto>> GetAllPresentEmployeesAsync(int companyId);

		IQueryable<GetEmployeeDto> AllPresentEmployees( int companyId );
	}
}
