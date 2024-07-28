using Microsoft.EntityFrameworkCore;
using Payroll.Models;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Mapper.CustomMap
{
	public interface IGetEmployeeMapping
	{
		IQueryable<GetEmployeeVM> MapPresentEmployeesQueryable
			( DbSet<Employee> employees, int companyId );

		public IQueryable<GetEmployeeVM> MapAllEmployeesQueryable
			( DbSet<Employee> employees, int companyId );
	}
}