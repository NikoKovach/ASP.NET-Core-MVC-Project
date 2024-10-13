using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IDiplomasCollectionFactory
       {
              Dictionary<string, IQueryable<DiplomaVM>> SortedDiplomasCollection { get; set; }

       }
}
