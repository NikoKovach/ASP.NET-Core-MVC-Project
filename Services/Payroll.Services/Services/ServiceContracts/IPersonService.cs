using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IPersonService : IAddUpdate<PersonVM>, IEntityName
       {
              IQueryable<SearchPersonVM> AllActive_SearchPersonVM();

              IQueryable<PersonVM> All();

              IQueryable<PersonVM> All( string? sortParam, PersonFilterVM? filter );
       }
}
