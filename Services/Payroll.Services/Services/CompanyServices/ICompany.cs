using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels;

namespace Payroll.Services.Services.CompanyServices
{
     public interface ICompany : IGetCompany,IAddUpdate<CompanyViewModel>
     {
		Task<ICollection<CompanyViewModel>> GetAllCompaniesAsync();

		Task<ICollection<CompanyViewModel>> GetAllValidCompaniesAsync();

		void CreateUpdateCompanyFolder(string rootFolder, 
			CompanyViewModel viewModel,string actionName,params string[] viewModelOld);
     }
}
