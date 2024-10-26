using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IAddressService : IAddUpdate<AddressVM>, IDelete
       {
              //IQueryable<ModelVM>? All( int? personId );

              Task<AddressesOfPersonVM>? AllRealAsync( int? personId );

              IQueryable<AddressVM>? AllAddresses( string? sortParam, SearchAddressVM? filter );
       }
}
