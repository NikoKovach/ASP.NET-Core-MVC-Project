using Payroll.ModelsDto;

namespace Payroll.Services.Services.CompanyServices
{
     public interface IGetCompany
     {
          Task<CompanyDto> GetActiveCompanyByUniqueIdAsync( string companyUniqueId );
     }
}
