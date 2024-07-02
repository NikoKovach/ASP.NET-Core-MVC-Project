using Microsoft.EntityFrameworkCore;
using Payroll.Models;
using Payroll.ModelsDto.EmployeeDtos;

namespace Payroll.Mapper.CustomMap
{
	public interface IGetEmployeeMapping
	{
		IQueryable<GetEmployeeDto> MapPresentEmployeesQueryable
			( DbSet<Employee> employees, int companyId );

		public IQueryable<GetEmployeeDto> MapAllEmployeesQueryable
			( DbSet<Employee> employees, int companyId );
	}
}