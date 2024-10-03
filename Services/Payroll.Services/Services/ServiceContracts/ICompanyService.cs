using Payroll.ViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface ICompanyService : IAddUpdate<CompanyViewModel>
       {
              IQueryable<CompanyViewModel> All();

              IQueryable<CompanyViewModel> AllActive();

              IQueryable<CompanyViewModel> AllActive( string companyId );

              IQueryable<SearchCompanyVM> AllActive_SearchCompanyVM();

              void CreateUpdateCompanyFolder( string rootFolder,
                     CompanyViewModel viewModel, string actionName, params string[] viewModelOld );
       }
}
