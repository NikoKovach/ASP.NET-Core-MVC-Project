using Payroll.Data.Common;
using Payroll.Models;
using Payroll.ViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
	public interface IValidateEmployeeVModels
	{
		ViewModelState EntityState { get; }

		void NumberFromTheListIsValid( string listNumber, int companyId, IRepository<Employee> repository );

	}
}
