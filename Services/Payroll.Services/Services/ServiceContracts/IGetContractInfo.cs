using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
    public interface IGetContractInfo
    {
        Task<ContractEmpVM> GetContractAsync(int? companyId, int empId);
    }
}