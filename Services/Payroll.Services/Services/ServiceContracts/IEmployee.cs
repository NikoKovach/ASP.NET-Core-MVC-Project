using Payroll.ModelsDto.EmployeeDtos;

namespace Payroll.Services.Services.ServiceContracts
{
	public interface IEmployee :IGetEmployees,IAddUpdate<EmployeeDto>
	{
	}
}
