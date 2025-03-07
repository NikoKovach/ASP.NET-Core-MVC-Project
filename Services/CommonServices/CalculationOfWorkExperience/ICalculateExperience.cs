using Payroll.ViewModels.EmployeeViewModels;

namespace LegalFramework.Services.CalculationOfWorkExperience
{
    public interface ICalculateExperience
    {
        Task<WorkExperienceVM> CalculateAsync( int? companyId, int empId );
    }
}