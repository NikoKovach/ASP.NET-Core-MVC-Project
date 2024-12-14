using Payroll.ViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface ICompanyService : IBasicAddUpdate<CompanyVM>, IBasicGetEntityIQueryable<string>
       {
              IQueryable<CompanyVM> All();

              IQueryable<CompanyVM> AllActive();

              IQueryable<CompanyVM> AllActive( string companyId );

              IQueryable<SearchCompanyVM> AllActive_SearchCompanyVM();

              void CreateUpdateCompanyFolder( string rootFolder,
                     CompanyVM viewModel, string actionName, params string[] viewModelOld );
       }
}
