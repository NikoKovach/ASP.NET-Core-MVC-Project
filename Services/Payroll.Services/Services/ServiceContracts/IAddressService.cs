using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IAddressService : IBasicAddUpdate<AddressVM>
       {
              Task<AddressesOfPersonVM>? AllRealAsync( int? personId );

              IQueryable<AddressVM>? AllAddresses( string? sortParam, SearchAddressVM? filter );

              Task AttachAddressAsync( int? personId, int? addressId, string? addressType );

              Task DetachAddressAsync( int? personId, string? addressType );

              Task AddAndAttachAsync( AddressVM addressVM );

              Task UpdateAndAttachAsync( AddressVM addressVM );
       }
}
