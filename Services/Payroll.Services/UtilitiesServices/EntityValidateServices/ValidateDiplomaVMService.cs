using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Payroll.Services.UtilitiesServices.Messages;
using Payroll.ViewModels;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.UtilitiesServices.EntityValidateServices
{
       public class ValidateDiplomaVMService : ValidateBaseClass, IValidate<ValidateBaseModel>
       {

              public override void Validate( ModelStateDictionary modelState, ValidateBaseModel viewModel,
                                                 [CallerMemberName] string actionName = "", params object[] parameters )
              {
                     base.ModelState = modelState;

                     DiplomaVM? diplomaVM = (DiplomaVM) viewModel;

                     MarkFieldsAsRequired( diplomaVM );
              }

              public override void Validate( ModelStateDictionary modelState,
                                                               IEnumerable<ValidateBaseModel> entitiesForEdit )
              {
                     base.ModelState = modelState;

                     List<DiplomaVM> changedDiplomas = (List<DiplomaVM>) entitiesForEdit;

                     MarkFieldsAsRequired( changedDiplomas );
              }

              //#####################################################################

              private void MarkFieldsAsRequired( DiplomaVM? diplomaVM, string? rootName = null )
              {
                     PropertyInfo[]? propertiesArr = diplomaVM.GetType().GetProperties();

                     List<PropertyInfo>? otherProps = propertiesArr
                                                                                 .Where( x => x.Name != nameof( diplomaVM.PersonId ) )
                                                                                 .ToList();

                     List<object> valuesList = new List<object>();

                     otherProps.ForEach( x =>
                     {
                            var value = x.GetValue( diplomaVM );
                            valuesList.Add( value );
                     } );

                     if ( !valuesList.All( x => x == null ) )
                     {
                            rootName ??= nameof( diplomaVM );

                            string propStringName = nameof( diplomaVM.DiplomaRegNumber );
                            SetRequiredError( diplomaVM.DiplomaRegNumber, rootName, propStringName, diplomaVM );

                            propStringName = nameof( diplomaVM.EducationTypeName );
                            SetRequiredError( diplomaVM.EducationTypeName, rootName, propStringName, diplomaVM );
                     }
              }

              private void SetRequiredError( string? propValue, string rootName, string propStringName, DiplomaVM diplomaVM )
              {
                     if ( string.IsNullOrEmpty( propValue ) )
                     {
                            string keyString = this.GetModelStateKeyString( rootName, propStringName );

                            string displayName = this.GetDisplayName( propStringName, diplomaVM );

                            base.ModelState.AddModelError( keyString,
                                   string.Format( OutputMessages.ErrorFieldIsRequired, displayName ) );
                     }
              }

              private void MarkFieldsAsRequired( List<DiplomaVM> entitiesForEdit )
              {
                     for ( int i = 0; i < entitiesForEdit.Count; i++ )
                     {
                            DiplomaVM? diplomaVM = entitiesForEdit[ i ];

                            string? rootName = $"{nameof( entitiesForEdit )}[{i}]";

                            MarkFieldsAsRequired( diplomaVM, rootName );
                     }
              }

              protected string GetDisplayName( string propertyName, DiplomaVM? diplomaVModel )
              {
                     PropertyInfo? property = diplomaVModel.GetType().GetProperty( propertyName );

                     string displayName = this.GetDisplayName( property );

                     return displayName;
              }
       }
}

