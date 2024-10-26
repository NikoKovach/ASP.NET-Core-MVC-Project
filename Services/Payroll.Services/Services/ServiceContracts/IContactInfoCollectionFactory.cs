using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IContactInfoCollectionFactory
       {
              IQueryable<ContactInfoVM>? SortedCollection( int? personId, string? sortParam );
       }
}
