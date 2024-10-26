using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IAddressesCollectionFactory
       {
              IQueryable<AddressVM>? SortedCollection( string? sortParam, SearchAddressVM? filter );
       }
}
