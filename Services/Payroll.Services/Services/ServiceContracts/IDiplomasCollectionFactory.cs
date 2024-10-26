using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IDiplomasCollectionFactory
       {
              IQueryable<DiplomaVM>? SortedCollection( int? personId, string? sortParam );
       }
}
