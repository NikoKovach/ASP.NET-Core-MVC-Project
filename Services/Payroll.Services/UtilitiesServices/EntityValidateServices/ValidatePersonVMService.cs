using System.Reflection;
using System.Runtime.CompilerServices;
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

              public override void Validate( ModelStateDictionary modelState, ValidateBaseModel viewModel,
                                                        [CallerMemberName] string actionName = "", params object[] parameters )
              {
                     base.ModelState = modelState;

                     PersonVM? personVM = (PersonVM) viewModel;

                     ValidateCivilId( personVM );
              }

              public override void Validate( ModelStateDictionary modelState,
                                                               IEnumerable<ValidateBaseModel> entitiesForEdit )
              {
                     base.ModelState = modelState;

                     List<PersonVM> changedPersons = (List<PersonVM>) entitiesForEdit;

                     HasAnotherPersonWithTheSameCivilNumber( changedPersons, nameof( entitiesForEdit ) );
              }

              //#####################################################################

              private void HasAnotherPersonWithTheSameCivilNumber(
                     IEnumerable<PersonVM> changedPersons, string entityRootName )
              {
                     this.FieldErrors.Clear();

                     List<PersonVM> modifiedModelsList = changedPersons.ToList();

                     for ( int i = 0; i < modifiedModelsList.Count; i++ )
                     {
                            PersonVM? item = modifiedModelsList[ i ];

                            Person? person = this.persons.Where( x => x.Id == item.Id )
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

                                          string keyString = $"{entityRootName}[{indexOfModifiedEntity}].{itemStringName}";

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
