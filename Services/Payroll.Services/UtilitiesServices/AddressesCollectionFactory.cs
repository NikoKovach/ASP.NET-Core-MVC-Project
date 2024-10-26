using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.UtilitiesServices
{
       public class AddressesCollectionFactory : IAddressesCollectionFactory
       {
              private IMapEntity mapper;
              private IRepository<Address> repository;
              private Dictionary<string, IQueryable<AddressVM>> sortedAddressesCollection;

              public AddressesCollectionFactory( IMapEntity mapper, IRepository<Address> repository )
              {
                     this.mapper = mapper;

                     this.repository = repository;
              }

              public IQueryable<AddressVM>? SortedCollection( string? sortParam, SearchAddressVM? filter )
              {
                     //filter --- country, region, city, street, number
                     //if ( string.IsNullOrEmpty( filter.Country )
                     //      && string.IsNullOrEmpty( filter.Region ) 
                     //      && string.IsNullOrEmpty( filter.City )
                     //       && string.IsNullOrEmpty( filter.Street ) 
                     //       && ( filter.Number is null || filter.Number < 1 ) )
                     //       filter = null;

                     IQueryable<Address>? addresses = this.repository.AllAsNoTracking();

                     SetAddressesDictionary( addresses );

                     if ( string.IsNullOrEmpty( sortParam ) )
                     {
                            return this.sortedAddressesCollection[ "default" ];
                     }

                     return this.sortedAddressesCollection[ sortParam.ToLower() ];
              }

              //#######################################################################
              private void SetAddressesDictionary( IQueryable<Address>? addresses )
              {
                     this.sortedAddressesCollection = new Dictionary<string, IQueryable<AddressVM>>();

                     this.sortedAddressesCollection[ "country_desc" ] = CountryDesc( addresses );
                     this.sortedAddressesCollection[ "country_asc" ] = CountryAsc( addresses );
                     this.sortedAddressesCollection[ "region_desc" ] = RegionDesc( addresses );
                     this.sortedAddressesCollection[ "region_asc" ] = RegionAsc( addresses );
                     this.sortedAddressesCollection[ "city_desc" ] = CityDesc( addresses );
                     this.sortedAddressesCollection[ "city_asc" ] = CityAsc( addresses );
                     this.sortedAddressesCollection[ "street_desc" ] = StreetDesc( addresses );
                     this.sortedAddressesCollection[ "street_asc" ] = StreetAsc( addresses );

                     this.sortedAddressesCollection[ "default" ] = DefaultAddressesCollection( addresses );
              }

              private IQueryable<AddressVM>? DefaultAddressesCollection( IQueryable<Address>? addresses )
              {
                     IQueryable<AddressVM>? defaultCollection =
                            this.mapper.ProjectTo<Address, AddressVM>( addresses );

                     return defaultCollection;
              }

              private IQueryable<AddressVM> CountryDesc( IQueryable<Address>? addresses )
              {
                     IQueryable<AddressVM>? sortedAddresses =
                          this.mapper.ProjectTo<Address, AddressVM>( addresses )
                                               .OrderByDescending( x => x.Country );

                     return sortedAddresses;
              }

              private IQueryable<AddressVM> CountryAsc( IQueryable<Address>? addresses )
              {
                     IQueryable<AddressVM>? sortedAddresses =
                            this.mapper.ProjectTo<Address, AddressVM>( addresses )
                                                 .OrderBy( x => x.Country );

                     return sortedAddresses;
              }

              private IQueryable<AddressVM> RegionDesc( IQueryable<Address>? addresses )
              {
                     IQueryable<AddressVM>? sortedAddresses =
                            this.mapper.ProjectTo<Address, AddressVM>( addresses )
                                                 .OrderByDescending( x => x.Region );

                     return sortedAddresses;
              }

              private IQueryable<AddressVM> RegionAsc( IQueryable<Address>? addresses )
              {
                     IQueryable<AddressVM>? sortedAddresses =
                            this.mapper.ProjectTo<Address, AddressVM>( addresses )
                                                 .OrderBy( x => x.Region );

                     return sortedAddresses;
              }

              private IQueryable<AddressVM> CityDesc( IQueryable<Address>? addresses )
              {
                     IQueryable<AddressVM>? sortedAddresses =
                            this.mapper.ProjectTo<Address, AddressVM>( addresses )
                                                 .OrderByDescending( x => x.City );

                     return sortedAddresses;
              }

              private IQueryable<AddressVM> CityAsc( IQueryable<Address>? addresses )
              {
                     IQueryable<AddressVM>? sortedAddresses =
                            this.mapper.ProjectTo<Address, AddressVM>( addresses )
                                                 .OrderBy( x => x.City );

                     return sortedAddresses;
              }

              private IQueryable<AddressVM> StreetDesc( IQueryable<Address>? addresses )
              {
                     IQueryable<AddressVM>? sortedAddresses =
                            this.mapper.ProjectTo<Address, AddressVM>( addresses )
                                                 .OrderByDescending( x => x.Street );

                     return sortedAddresses;
              }

              private IQueryable<AddressVM> StreetAsc( IQueryable<Address>? addresses )
              {
                     IQueryable<AddressVM>? sortedAddresses =
                            this.mapper.ProjectTo<Address, AddressVM>( addresses )
                                                 .OrderBy( x => x.Street );

                     return sortedAddresses;
              }
       }
}

