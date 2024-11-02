using System.Runtime.CompilerServices;
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

              public override void Validate( ModelStateDictionary modelState, ValidateBaseModel viewModel,
                                                        [CallerMemberName] string actionName = "", params object[] parameters )
              {
                     base.ModelState = modelState;

                     AddressVM? addressVM = (AddressVM) viewModel;

                     if ( actionName.Equals( "Create" ) )
                     {
                            AddressIdIsCreaterThanZero( addressVM );
                     }

                     if ( actionName.Equals( "Edit" ) )
                     {
                            AddressIdExistsInDatabase( addressVM );
                     }

                     if ( actionName.Equals( "EditAttach" ) )
                     {
                            AddressIdExistsInDatabase( addressVM );

                            AddressTypeIsSelected( addressVM );
                     }

                     if ( actionName.Equals( "CreateAttach" ) )
                     {
                            AddressIdIsCreaterThanZero( addressVM );

                            AddressTypeIsSelected( addressVM );
                     }
              }

              //*******************************************************************

              private void AddressIdIsCreaterThanZero( AddressVM addressVM )
              {
                     this.FieldErrors.Clear();

                     if ( addressVM.Id > 0 )
                     {
                            string modelStateKey = $"{nameof( addressVM )}.{nameof( addressVM.Id )}";

                            string error = string.Format( OutputMessages.ErrorIDIsNotZero );

                            this.FieldErrors.Add( error );

                            AddModelStateError( nameof( addressVM.Id ), modelStateKey );
                     }
              }

              private void AddressIdExistsInDatabase( AddressVM addressVM )
              {
                     this.FieldErrors.Clear();

                     bool addressIdExists = this.repository.AllAsNoTracking()
                                                                                       .Any( x => x.Id == addressVM.Id );
                     if ( !addressIdExists )
                     {
                            string modelStateKey = $"{nameof( addressVM )}.{nameof( addressVM.Id )}";

                            string error = string.Format( OutputMessages.ErrorAddressIdNotExists );

                            this.FieldErrors.Add( error );

                            AddModelStateError( nameof( addressVM.Id ), modelStateKey );
                     }
              }

              private void AddressTypeIsSelected( AddressVM addressVM )
              {
                     this.FieldErrors.Clear();

                     if ( string.IsNullOrEmpty( addressVM.AddressType ) )
                     {
                            string modelStateKey = $"{nameof( addressVM )}.{nameof( addressVM.AddressType )}";

                            string error = string.Format( OutputMessages.ErrorAddressType );

                            this.FieldErrors.Add( error );

                            AddModelStateError( nameof( addressVM.AddressType ), modelStateKey );
                     }
              }
       }
}
