using Payroll.ViewModels;

namespace Payroll.Services.Services.CompanyServices
{
     public interface IGetCompany
     {
          Task<CompanyViewModel> GetActiveCompanyByUniqueIdAsync( string companyUniqueId );
     }
}
