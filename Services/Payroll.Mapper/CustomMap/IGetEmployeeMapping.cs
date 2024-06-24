using Payroll.Data;
using Payroll.ModelsDto.EmployeeDtos;

namespace Payroll.Mapper.CustomMap
{
	public interface IGetEmployeeMapping
	{
		IQueryable<GetEmployeeDto> MapPresentEmployeesQueryable
			( PayrollContext db, int companyId );

		public IQueryable<GetEmployeeDto> MapAllEmployeesQueryable
			( PayrollContext db, int companyId );
	}
}