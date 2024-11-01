﻿using Microsoft.EntityFrameworkCore;
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

              public async Task AttachAddressAsync( int? personId, int? addressId, string? addressType )
              {
                     Person? person = await this.personRepository.All()
                                                                                                       .Where( x => x.Id == personId )
                                                                                                       .FirstOrDefaultAsync();

                     if ( addressType.Equals( AddressType.Permanent.ToString() ) )
                     {
                            person.PermanentAddressId = addressId;
                     }
                     else if ( addressType.Equals( AddressType.Current.ToString() ) )
                     {
                            person.CurrentAddressId = addressId;
                     }

                     await this.personRepository.SaveChangesAsync();
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
                     var address = this.mapper.Map<AddressVM, Address>( viewModel );

                     Address? permanentModel = await this.personRepository
                                                                                    .AllAsNoTracking()
                                                                                    .Where( x => x.Id == viewModel.PersonId )
                                                                                    .Select( x => x.PermanentAddress )
                                                                                    .FirstOrDefaultAsync();
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

              private void AttachAddress()
              {
                     //Person? person = await this.personRepository.All()
                     //                               .Where( x => x.Id == viewModel.PersonId )
                     //                               .FirstOrDefaultAsync();

                     //if ( IsExist( address ) )
                     //{
                     //       personRepository.Context.Addresses.Entry( address ).State = EntityState.Unchanged;
                     //}
                     //else
                     //{
                     //       personRepository.Context.Addresses.Entry( address ).State = EntityState.Added;
                     //}

                     //if ( viewModel.AddressType.Equals( "permanent" ) )
                     //{
                     //       person.PermanentAddress = address;
                     //}
                     //else if ( viewModel.AddressType.Equals( "current" ) )
                     //{
                     //       person.CurrentAddress = address;
                     //}
              }
       }
}

