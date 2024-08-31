using Payroll.ViewModels;

namespace Payroll.Services.Services.CompanyServices
{
       public interface IPerson
       {
              IQueryable<SearchPersonVM> AllActive_SearchPersonVM();
       }
}
