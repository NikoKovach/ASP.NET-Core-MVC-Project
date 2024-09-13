using Payroll.ViewModels;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IPerson : IAddUpdate<PersonViewModel>
       {
              IQueryable<SearchPersonVM> AllActive_SearchPersonVM();

              IQueryable<PersonViewModel> All();

              IQueryable<PersonViewModel> All( string? sortParam );
       }
}
