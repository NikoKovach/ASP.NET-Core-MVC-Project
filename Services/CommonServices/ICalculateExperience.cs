using Payroll.ViewModels.EmployeeViewModels;

namespace LegalFramework.Services
{
	public interface ICalculateExperience
	{
		Task<WorkExperienceVM> CalculateAsync( int? companyId, int empId );
	}
}