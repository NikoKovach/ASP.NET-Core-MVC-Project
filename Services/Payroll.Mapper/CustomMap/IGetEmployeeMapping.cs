using Payroll.Data;
using Payroll.ModelsDto.EmployeeDtos;

namespace Payroll.Mapper.CustomMap
{
	public interface IGetEmployeeMapping
	{
		Task<IList<GetEmployeeDto>> MapAllEmployeesAsync( PayrollContext db,
			int companyId );

		Task<IList<GetEmployeeDto>> MapPresentEmployeesAsync( PayrollContext db,
			int companyId );

		IQueryable<GetEmployeeDto> MapPresentEmployeesQueryable
			( PayrollContext db, int companyId );
	}
}