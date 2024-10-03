using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Payroll.Data.Common;
using Payroll.Models;
using Payroll.Services.UtilitiesServices.Messages;
using Payroll.ViewModels;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.UtilitiesServices.EntityValidateServices
{
       public class ValidatePersonVMService : ValidateBaseClass, IValidate<ValidateBaseModel>
       {
              private IQueryable<Person> persons;

              public ValidatePersonVMService( IRepository<Person> personsRepo )
              {
                     this.persons = personsRepo.AllAsNoTracking();
              }

              public override void Validate( ModelStateDictionary modelState, ValidateBaseModel viewModel )
              {
                     //key = personVM.MiddleName
                     base.ModelState = modelState;

                     PersonVM? personVM = (PersonVM) viewModel;

                     MarkFieldsAsRequired( personVM );

                     ValidateCivilId( personVM );
              }

              public override void Validate( ModelStateDictionary modelState,
                                                               IEnumerable<ValidateBaseModel> entitiesForEdit )
              {
                     base.ModelState = modelState;

                     List<PersonVM> changedPersons = (List<PersonVM>) entitiesForEdit;

                     HasAnotherPersonWithTheSameCivilNumber( changedPersons, nameof( entitiesForEdit ) );

                     MarkFieldsAsRequired( changedPersons );
              }

              //#####################################################################

              private void HasAnotherPersonWithTheSameCivilNumber( IEnumerable<PersonVM> changedPersons,
                                                                                           string entityRootName )
              {
                     this.FieldErrors.Clear();

                     List<PersonVM> modifiedModelsList = changedPersons.ToList();

                     for ( int i = 0; i < modifiedModelsList.Count; i++ )
                     {
                            PersonVM? item = modifiedModelsList[ i ];

                            Person? person = this.persons
                                                                  .Where( x => x.Id == item.Id )
                                                                  .FirstOrDefault();

                            if ( item.CivilNumber != person.EGN )
                            {
                                   string? personCivilId = this.persons
                                                                 .Where( x => x.Id != item.Id && x.EGN.Equals( item.CivilNumber ) )
                                                                 .Select( x => x.EGN )
                                                                 .FirstOrDefault();

                                   if ( !string.IsNullOrEmpty( personCivilId ) )
                                   {
                                          string errorIsExists = FormatErrorMessage( item.CivilNumber, OutputMessages.ErrorValueExists );

                                          this.FieldErrors.Add( errorIsExists );

                                          int indexOfModifiedEntity = i;
                                          string itemStringName = nameof( item.CivilNumber );

                                          string keyString = base.GetModelStateKeyString( entityRootName, indexOfModifiedEntity,
                                                                                                                                                                        itemStringName );

                                          string displayName = this.GetDisplayName( itemStringName, item );

                                          this.AddModelStateError( displayName, keyString );
                                   }
                            }
                     }
              }

              protected string GetDisplayName( string propertyName, PersonVM? personVModel )
              {
                     PropertyInfo? property = GetPorpertyInfo( personVModel, propertyName );

                     string displayName = this.GetDisplayName( property );

                     return displayName;
              }

              private void MarkFieldsAsRequired( PersonVM? personVM, string? rootName = null ) // rootName come like parameter
              {
                     if ( rootName == null )
                     {
                            rootName = nameof( personVM );
                     }

                     var propertiesArr = personVM.GetType().GetProperties();

                     List<object> valuesList = new List<object>();

                     foreach ( var item in propertiesArr )
                     {
                            string? name = item.Name;
                            object? propValue = item.GetValue( personVM );
                            valuesList.Add( propValue );
                     }

                     bool allValuesAreNull = valuesList.All( x => x == null );

                     if ( !allValuesAreNull )
                     {
                            // property value :  personVM.FirstName ; personVM.LastName
                            //string rootName
                            //string properyName
                            //PersonVM personVM
                            string propStringName = nameof( personVM.FirstName );
                            SetRequiredError( personVM.FirstName, rootName, propStringName, personVM );

                            propStringName = nameof( personVM.LastName );
                            SetRequiredError( personVM.LastName, rootName, propStringName, personVM );

                            propStringName = nameof( personVM.GenderType );
                            SetRequiredError( personVM.GenderType, rootName, propStringName, personVM );

                            propStringName = nameof( personVM.CivilNumber );
                            SetRequiredError( personVM.CivilNumber, rootName, propStringName, personVM );
                     }
              }

              private void MarkFieldsAsRequired( List<PersonVM> entitiesForEdit )
              {
                     for ( int i = 0; i < entitiesForEdit.Count; i++ )
                     {
                            PersonVM? personVM = (PersonVM) entitiesForEdit[ i ];

                            string? rootName = $"{nameof( entitiesForEdit )}[{i}]";

                            MarkFieldsAsRequired( personVM, rootName );
                     }

              }

              private void ValidateCivilId( PersonVM? personVM )
              {
                     this.FieldErrors.Clear();

                     string rootName = nameof( personVM );
                     string stringPropName = nameof( personVM.CivilNumber );

                     PropertyInfo? civilIdProperty = GetPorpertyInfo( personVM, stringPropName );

                     string? personCivilId = this.persons
                                                                 .Where( x => x.EGN.Equals( personVM.CivilNumber ) )
                                                                 .Select( x => x.EGN )
                                                                 .FirstOrDefault();

                     if ( !string.IsNullOrEmpty( personCivilId ) )
                     {
                            string errorIsExists = FormatErrorMessage( personVM.CivilNumber, OutputMessages.ErrorValueExists );

                            this.FieldErrors.Add( errorIsExists );

                            string keyString = this.GetModelStateKeyString( rootName, stringPropName );

                            string displayName = this.GetDisplayName( civilIdProperty );

                            this.AddModelStateError( displayName, keyString );
                     }
              }

              private void SetRequiredError( string? propValue, string rootName, string propStringName, PersonVM personVM )
              {
                     if ( string.IsNullOrEmpty( propValue ) )
                     {
                            string keyString = this.GetModelStateKeyString( rootName, propStringName );

                            string displayFirstName = this.GetDisplayName( propStringName, personVM );

                            base.ModelState.AddModelError( keyString,
                                   FormatErrorMessage( displayFirstName, OutputMessages.ErrorFieldIsRequired ) );
                     }
              }

              private string FormatErrorMessage( string propertyName, string errorMsg )
              {
                     //OutputMessages.ErrorFieldIsRequired
                     return string.Format( errorMsg, propertyName );
              }

              private PropertyInfo? GetPorpertyInfo( PersonVM? viewModel, string propertyName )
              {
                     return viewModel.GetType().GetProperty( propertyName );
              }
       }
}
