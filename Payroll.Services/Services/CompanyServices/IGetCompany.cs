
using Payroll.ModelsDto;
using Payroll.Services.Services.ServiceContracts;

namespace Payroll.Services.Services.CompanyServices
{
     public interface IGetCompany : IGetEntities<CompanyDto>
     {
          CompanyDto GetActiveCompanyByUniqueId( string companyUniqueId );
     }
}
