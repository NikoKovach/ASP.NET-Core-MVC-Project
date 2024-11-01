using System.Reflection;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.UtilitiesServices
{
       public class FactoryAddressesCollection : IFactorySortCollection<AddressVM>
       {
              private IMapEntity mapper;
              private IRepository<Address> repository;
              private Dictionary<string, IQueryable<AddressVM>> sortedAddressesCollection;

              public FactoryAddressesCollection( IMapEntity mapper, IRepository<Address> repository )
              {
                     this.mapper = mapper;

                     this.repository = repository;
              }

              public IQueryable<AddressVM>? SortedCollection( string? sortParam, params object[] items )
              {
                     SearchAddressVM? filter = ( items.Length > 0 ) ? (SearchAddressVM?) items[ 0 ] : new SearchAddressVM();

                     IQueryable<Address>? addresses = FilterAddresses( filter );

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
                                               .OrderByDescending( x => x.Country )
                                               .ThenByDescending( x => x.Region )
                                               .ThenByDescending( x => x.City )
                                               .ThenByDescending( x => x.Street );

                     return sortedAddresses;
              }

              private IQueryable<AddressVM> CountryAsc( IQueryable<Address>? addresses )
              {
                     IQueryable<AddressVM>? sortedAddresses =
                            this.mapper.ProjectTo<Address, AddressVM>( addresses )
                                                 .OrderBy( x => x.Country )
                                                 .ThenBy( x => x.Region )
                                                 .ThenBy( x => x.City )
                                                 .ThenBy( x => x.Street );

                     return sortedAddresses;
              }

              private IQueryable<AddressVM> RegionDesc( IQueryable<Address>? addresses )
              {
                     IQueryable<AddressVM>? sortedAddresses =
                            this.mapper.ProjectTo<Address, AddressVM>( addresses )
                                                 .OrderByDescending( x => x.Region )
                                                 .ThenByDescending( x => x.City )
                                                 .ThenByDescending( x => x.Street );

                     return sortedAddresses;
              }

              private IQueryable<AddressVM> RegionAsc( IQueryable<Address>? addresses )
              {
                     IQueryable<AddressVM>? sortedAddresses =
                            this.mapper.ProjectTo<Address, AddressVM>( addresses )
                                                 .OrderBy( x => x.Region )
                                                 .ThenBy( x => x.City )
                                                 .ThenBy( x => x.Street );

                     return sortedAddresses;
              }

              private IQueryable<AddressVM> CityDesc( IQueryable<Address>? addresses )
              {
                     IQueryable<AddressVM>? sortedAddresses =
                            this.mapper.ProjectTo<Address, AddressVM>( addresses )
                                                 .OrderByDescending( x => x.City )
                                                 .ThenByDescending( x => x.Street )
                                                 .ThenByDescending( x => x.Number );

                     return sortedAddresses;
              }

              private IQueryable<AddressVM> CityAsc( IQueryable<Address>? addresses )
              {
                     IQueryable<AddressVM>? sortedAddresses =
                            this.mapper.ProjectTo<Address, AddressVM>( addresses )
                                                 .OrderBy( x => x.City )
                                                 .ThenBy( x => x.Street )
                                                 .ThenBy( x => x.Number );

                     return sortedAddresses;
              }

              private IQueryable<AddressVM> StreetDesc( IQueryable<Address>? addresses )
              {
                     IQueryable<AddressVM>? sortedAddresses =
                            this.mapper.ProjectTo<Address, AddressVM>( addresses )
                                                 .OrderByDescending( x => x.Street )
                                                 .ThenByDescending( x => x.Number )
                                                 .ThenByDescending( x => x.City );

                     return sortedAddresses;
              }

              private IQueryable<AddressVM> StreetAsc( IQueryable<Address>? addresses )
              {
                     IQueryable<AddressVM>? sortedAddresses =
                            this.mapper.ProjectTo<Address, AddressVM>( addresses )
                                                 .OrderBy( x => x.Street )
                                                 .ThenBy( x => x.Number )
                                                 .ThenBy( x => x.City );

                     return sortedAddresses;
              }

              private bool HasData( SearchAddressVM? filter )
              {
                     PropertyInfo[]? propertiesList = filter.GetType().GetProperties();

                     List<object?>? propertyValues = propertiesList.Select( x => x.GetValue( filter ) ).ToList();

                     return propertyValues.Any( x => x != null );
              }

              private IQueryable<Address>? FilterAddresses( SearchAddressVM? filter )
              {
                     IQueryable<Address>? resultAddresses = this.repository.AllAsNoTracking();

                     if ( HasData( filter ) )
                     {
                            if ( !string.IsNullOrEmpty( filter.Country ) )
                            {
                                   resultAddresses = resultAddresses.Where( x => x.Country.Contains( filter.Country ) );
                            }

                            if ( !string.IsNullOrEmpty( filter.Region ) )
                            {
                                   resultAddresses = resultAddresses.Where( x => x.Region.Contains( filter.Region ) );
                            }

                            if ( !string.IsNullOrEmpty( filter.City ) )
                            {
                                   resultAddresses = resultAddresses.Where( x => x.City.Contains( filter.City ) );
                            }

                            if ( !string.IsNullOrEmpty( filter.Street ) )
                            {
                                   resultAddresses = resultAddresses.Where( x => x.Street.Contains( filter.Street ) );
                            }

                            if ( filter.Number != null && filter.Number > 0 )
                            {
                                   resultAddresses = resultAddresses.Where( x => x.Number == filter.Number );
                            }

                            return resultAddresses;
                     }

                     return this.repository.AllAsNoTracking();
              }
       }
}
