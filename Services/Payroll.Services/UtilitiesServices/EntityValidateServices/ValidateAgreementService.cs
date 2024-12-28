using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Payroll.Services.UtilitiesServices.Messages;
using Payroll.ViewModels;
using Payroll.ViewModels.EmpContractViewModels;

namespace Payroll.Services.UtilitiesServices.EntityValidateServices
{
       public class ValidateAgreementService : ValidateBaseClass, IValidate<ValidateBaseModel>
       {

              public override void Validate( ModelStateDictionary modelState, ValidateBaseModel viewModel,
                                                 [CallerMemberName] string actionName = "", params object[] parameters )
              {
                     // if contract is registered (isRegistereg  = true ) ,it cann't be change directly -> ModelState.isValid = false
                     base.ModelState = modelState;

                     LaborAgreementVM? agreementVM = (LaborAgreementVM?) viewModel;

                     ContractCanBeEdit( agreementVM );
              }

              //#####################################################################


              private void ContractCanBeEdit( LaborAgreementVM? agreementVM )
              {
                     this.ModelState.Clear();

                     bool registered = ( agreementVM.IsRegistered != null ) ? (bool) agreementVM.IsRegistered : false;

                     if ( registered )
                     {
                            string keyString = ( !string.IsNullOrEmpty( agreementVM.ViewTableRow ) )
                                   ? agreementVM.ViewTableRow : nameof( agreementVM.IsRegistered );

                            string errorMessage =
                                   string.Format( OutputMessages.ErrorContractCannotBeEdit, agreementVM.ContractNumber );

                            this.ModelState.AddModelError( keyString, errorMessage );
                     }
              }

              protected string GetDisplayName( string propertyName, LaborAgreementVM? agreementVM )
              {
                     PropertyInfo? property = agreementVM.GetType().GetProperty( propertyName );

                     string displayName = this.GetDisplayName( property );

                     return displayName;
              }
       }
}

