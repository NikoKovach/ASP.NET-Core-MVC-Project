using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
	public interface IEmployee :IGetEmployees,IAddUpdate<EmployeeVM>
	{

	}
}
