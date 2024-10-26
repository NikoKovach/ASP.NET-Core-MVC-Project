using Microsoft.EntityFrameworkCore;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services
{
       public class AddressService : IAddressService
       {
              private IRepository<Person> personRepository;
              private IMapEntity mapper;
              private IAddressesCollectionFactory addressesListFactory;

              public AddressService( IRepository<Person> personRepo, IMapEntity mapper,
                     IAddressesCollectionFactory addressesFactory )
              {
                     this.personRepository = personRepo;

                     this.mapper = mapper;

                     this.addressesListFactory = addressesFactory;
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
                     IQueryable<AddressVM>? sortedList = this.addressesListFactory.SortedCollection( sortParam, filter );

                     return sortedList;
              }

              public async Task AddAsync( AddressVM viewModel )
              {
                     Address? address = this.mapper.Map<AddressVM, Address>( viewModel );

                     Person? person = await this.personRepository.All()
                                                    .Where( x => x.Id == viewModel.PersonId )
                                                    .FirstOrDefaultAsync();

                     if ( IsExist( address ) )
                     {
                            personRepository.Context.Addresses.Entry( address ).State = EntityState.Unchanged;
                     }
                     else
                     {
                            personRepository.Context.Addresses.Entry( address ).State = EntityState.Added;
                     }

                     if ( viewModel.AddressType.Equals( "permanent" ) )
                     {
                            person.PermanentAddress = address;
                     }
                     else if ( viewModel.AddressType.Equals( "current" ) )
                     {
                            person.CurrentAddress = address;
                     }

                     await personRepository.SaveChangesAsync();
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

              public async Task UpdateAsync( ICollection<AddressVM> viewModel )
              {
                     return;
              }

              public async Task DeleteAsync( int? entityId, int? parentEntityId = null )
              {
                     return;
              }

              //******************************************************************

              private bool IsExist( Address addressVM )
              {
                     string? modelCountry = addressVM.Country;
                     string? modelRegion = addressVM.Region;
                     string? modelCity = addressVM.City;
                     string? modelSreet = addressVM.Street;
                     int? modelNumber = addressVM.Number;

                     List<Address>? addresses = this.personRepository.Context.Addresses
                                                                           .Where( x => x.Country.Equals( modelCountry )
                                                                                            && x.Region.Equals( modelRegion )
                                                                                            && x.City.Equals( modelCity )
                                                                                            && x.Street.Equals( modelSreet )
                                                                                            && x.Number == modelNumber
                                                                                         )
                                                                           .ToList();

                     string propName = addressVM.GetType().Name;

                     foreach ( var address in addresses )
                     {
                            if ( address.ApartmentNumber == addressVM.ApartmentNumber )
                            {
                                   return true;
                            }
                     }

                     return false;
              }
       }
}


//var permanentVM = this.mapper.Map<Model, ModelVM>( permanentModel );
//var currentVM = this.mapper.Map<Model, ModelVM>( currentModel );

//permanentVM.PersonId = personId;
//currentVM.PersonId = personId;

//public IQueryable<ModelVM>? All( int? personId )
//{
//       IQueryable<Diploma>? diplomas = repository.AllAsNoTracking()
//                                                                                        .Where( x => x.PersonId == personId );

//       IQueryable<DiplomaVM>? diplomasVM = mapper.ProjectTo<Diploma, DiplomaVM>( diplomas );

//       return null;
//}

//AllReal()
//if ( personId == null )
//{
//       return null;
//}

//IQueryable<DiplomaVM>? sortedDiplomas = this.diplomaSortFactory.SortedCollection( personId, sortParam );

//return null;