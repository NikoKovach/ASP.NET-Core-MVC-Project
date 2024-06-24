using Payroll.ModelsDto;
using Payroll.Services.Services.ServiceContracts;

namespace Payroll.Services.Services.CompanyServices
{
     public interface ICompany : IGetCompany,IAddUpdate<CompanyDto>
     {
		Task<ICollection<CompanyDto>> GetAllCompaniesAsync();

		Task<ICollection<CompanyDto>> GetAllValidCompaniesAsync();
     }
}
