using Microsoft.EntityFrameworkCore;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Models.EnumTables;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services
{
       public class AddressService : IAddressService
       {
              private IRepository<Person> personRepository;
              private IMapEntity mapper;
              private IFactorySortCollection<AddressVM> addressesFactory;

              public AddressService( IRepository<Person> personRepo, IMapEntity mapper,
                     IFactorySortCollection<AddressVM> addressesFactory )
              {
                     this.personRepository = personRepo;

                     this.mapper = mapper;

                     this.addressesFactory = addressesFactory;
              }

              public async Task<AddressesOfPersonVM>? AllRealAsync( int? personId )
              {
                     Address? permanentModel = await this.personRepository
                                                                                     .AllAsNoTracking()
                                                                                     .Where( x => x.Id == personId )
                                                                                     .Select( x => x.PermanentAddress )
                                                                                     .FirstOrDefaultAsync();

                     Address? currentAddress = await this.personRepository
                                                                              .AllAsNoTracking()
                                                                              .Where( x => x.Id == personId )
                                                                              .Select( x => x.CurrentAddress )
                                                                              .FirstOrDefaultAsync();

                     var personAddresses = new AddressesOfPersonVM
                     {
                            PersonId = personId,
                            PermanentAddress = permanentModel?.GetAddress,
                            CurrentAddress = currentAddress?.GetAddress,
                     };

                     return personAddresses;
              }

              public IQueryable<AddressVM>? AllAddresses( string? sortParam, SearchAddressVM? filter )
              {
                     IQueryable<AddressVM>? sortedList = this.addressesFactory.SortedCollection( sortParam, filter );

                     return sortedList;
              }

              public IQueryable<string>? GetEntity( int? addressId )
              {
                     IQueryable<string>? fullAddress = this.addressesFactory
                                                                                         .SortedCollection( string.Empty )
                                                                                         .Where( x => x.Id == addressId )
                                                                                         .Select( x => x.ToString() );

                     return fullAddress;
              }

              public IQueryable<AddressVM>? GetEntity<AddressVM>( int? addressId )
              {
                     IQueryable<AddressVM>? addressVM =
                            (IQueryable<AddressVM>?) this.addressesFactory
                                                                                     .SortedCollection( string.Empty )
                                                                                     .Where( x => x.Id == addressId );

                     return addressVM;
              }

              public async Task AttachAddressAsync( int? personId, int? addressId, string? addressType )
              {
                     Person? person = await this.personRepository.All()
                                                                                                       .Where( x => x.Id == personId )
                                                                                                       .FirstOrDefaultAsync();

                     if ( person.Id > 0 && addressId > 0 && !string.IsNullOrEmpty( addressType ) )
                     {
                            AttachAddressToPerson( addressType, person, addressId.GetValueOrDefault() );

                            await this.personRepository.SaveChangesAsync();
                     }
              }

              public async Task DetachAddressAsync( int? personId, string? addressType )
              {
                     Person? person = await this.personRepository.All()
                                                                                                       .Where( x => x.Id == personId )
                                                                                                       .FirstOrDefaultAsync();

                     if ( addressType.Equals( AddressType.Permanent.ToString() ) )
                     {
                            if ( person.PermanentAddressId != null )
                            {
                                   person.PermanentAddressId = null;
                            }
                     }
                     else if ( addressType.Equals( AddressType.Current.ToString() ) )
                     {
                            if ( person.CurrentAddressId != null )
                            {
                                   person.CurrentAddressId = null;
                            }
                     }

                     EntityState personState = this.personRepository.Context.Entry( person ).State;

                     if ( personState == EntityState.Modified )
                     {
                            await this.personRepository.SaveChangesAsync();
                     }
              }

              public async Task AddAsync( AddressVM viewModel )
              {
                     Address? address = this.mapper.Map<AddressVM, Address>( viewModel );

                     if ( address != null )
                     {
                            this.personRepository.Context.Addresses.Add( address );

                            await personRepository.SaveChangesAsync();
                     }
              }

              public async Task UpdateAsync( AddressVM viewModel )
              {
                     Address? address = this.mapper.Map<AddressVM, Address>( viewModel );

                     if ( !IsExist( address ) )
                     {
                            this.personRepository.Context.Entry( address ).State = EntityState.Modified;

                            await personRepository.SaveChangesAsync();
                     }
              }

              public async Task AddAndAttachAsync( AddressVM addressVM )
              {
                     Person? person = await this.personRepository.All()
                                                    .Where( x => x.Id == addressVM.PersonId )
                                                    .FirstOrDefaultAsync();

                     Address? address = this.mapper.Map<AddressVM, Address>( addressVM );

                     if ( IsExist( address ) )
                     {
                            this.personRepository.Context.Addresses.Entry( address ).State = EntityState.Unchanged;
                     }
                     else
                     {
                            this.personRepository.Context.Addresses.Entry( address ).State = EntityState.Added;
                     }

                     if ( person.Id > 0 && address.Id < 1 && !string.IsNullOrEmpty( addressVM.AddressType ) )
                     {
                            CreateAttachAddressToPerson( addressVM.AddressType, person, address );

                            await this.personRepository.SaveChangesAsync();
                     }
              }

              public async Task UpdateAndAttachAsync( AddressVM addressVM )
              {
                     //person = null - > addressVM.PersonId = 22 
                     Person? person = await this.personRepository.All()
                                                    .Where( x => x.Id == addressVM.PersonId )
                                                    .FirstOrDefaultAsync();

                     Address? address = this.mapper.Map<AddressVM, Address>( addressVM );

                     if ( !IsExist( address ) )
                     {
                            this.personRepository.Context.Entry( address ).State = EntityState.Modified;
                     }
                     else
                     {
                            this.personRepository.Context.Addresses.Entry( address ).State = EntityState.Unchanged;
                     }

                     if ( person.Id > 0 && address.Id > 0 && !string.IsNullOrEmpty( addressVM.AddressType ) )
                     {
                            AttachAddressToPerson( addressVM.AddressType, person, address.Id );

                            await this.personRepository.SaveChangesAsync();
                     }
              }

              //******************************************************************

              private bool IsExist( Address address )
              {
                     string? modelCountry = address.Country;
                     string? modelRegion = address.Region;
                     string? modelCity = address.City;
                     string? modelSreet = address.Street;
                     int? modelNumber = address.Number;

                     List<Address>? addresses = this.personRepository.Context.Addresses
                                                                           .AsNoTracking()
                                                                           .Where( x => x.Country.Equals( modelCountry )
                                                                                            && x.Region.Equals( modelRegion )
                                                                                            && x.City.Equals( modelCity )
                                                                                            && x.Street.Equals( modelSreet )
                                                                                            && x.Number == modelNumber
                                                                                         )
                                                                           .ToList();

                     foreach ( var item in addresses )
                     {
                            if ( item.ApartmentNumber == address.ApartmentNumber )
                            {
                                   return true;
                            }
                     }

                     return false;
              }

              private void AttachAddressToPerson( string addressType, Person? person, int addressId )
              {
                     if ( addressType.Equals( AddressType.Permanent.ToString() ) )
                     {
                            person.PermanentAddressId = addressId;
                     }
                     else if ( addressType.Equals( AddressType.Current.ToString() ) )
                     {
                            person.CurrentAddressId = addressId;
                     }
              }

              private void CreateAttachAddressToPerson( string addressType, Person? person, Address address )
              {
                     if ( addressType.Equals( AddressType.Permanent.ToString() ) )
                     {
                            person.PermanentAddress = address;
                     }
                     else if ( addressType.Equals( AddressType.Current.ToString() ) )
                     {
                            person.CurrentAddress = address;
                     }
              }
       }
}




