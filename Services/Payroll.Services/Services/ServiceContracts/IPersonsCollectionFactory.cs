using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IPersonsCollectionFactory
       {
              Dictionary<string, IQueryable<PersonVM>> SortedPersonsCollection { get; set; }

              IQueryable<PersonVM> Filtrate( PersonFilterVM? filter, string? sort );
       }
}
