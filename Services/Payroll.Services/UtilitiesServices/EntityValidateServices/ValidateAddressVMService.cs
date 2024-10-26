using Microsoft.AspNetCore.Mvc.ModelBinding;
using Payroll.Data.Common;
using Payroll.Models;
using Payroll.Services.UtilitiesServices.Messages;
using Payroll.ViewModels;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.UtilitiesServices.EntityValidateServices
{
       public class ValidateAddressVMService : ValidateBaseClass, IValidate<ValidateBaseModel>
       {
              private IRepository<Address> repository;

              public ValidateAddressVMService( IRepository<Address> ModelRepo )
              {
                     this.repository = ModelRepo;
              }

              public override void Validate( ModelStateDictionary modelState, ValidateBaseModel viewModel )
              {
                     base.ModelState = modelState;

                     AddressVM? addressVM = (AddressVM) viewModel;

                     IsExist( addressVM );
              }

              //*******************************************************************

              private void IsExist( AddressVM addressVM )
              {
                     this.FieldErrors.Clear();

                     string? modelCountry = addressVM.Country;
                     string? modelRegion = addressVM.Region;
                     string? modelCity = addressVM.City;
                     string? modelSreet = addressVM.Street;
                     int? modelNumber = addressVM.Number;

                     List<Address>? addresses = this.repository.AllAsNoTracking()
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
                                   string error = string.Format( OutputMessages.ErrorAddressExists );

                                   this.FieldErrors.Add( error );

                                   AddModelStateError( propName );

                                   break;
                            }
                     }
              }
       }
}
