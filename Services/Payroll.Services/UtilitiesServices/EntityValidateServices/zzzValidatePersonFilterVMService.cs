//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Payroll.Data.Common;
//using Payroll.Models;
//using Payroll.Services.UtilitiesServices.Messages;
//using Payroll.ViewModels;
//using Payroll.ViewModels.PersonViewModels;

//namespace Payroll.Services.UtilitiesServices.EntityValidateServices
//{
//       public class ValidatePersonFilterVMService : ValidateBaseClass, IValidate<ValidateBaseModel>
//       {
//              private IQueryable<Person> persons;

//              public ValidatePersonFilterVMService( IRepository<Person> personsRepo )
//              {
//                     this.persons = personsRepo.AllAsNoTracking();
//              }

//              public override void Validate( ModelStateDictionary modelState, ValidateBaseModel? viewModel )
//              {
//                     base.ModelState = modelState;

//                     PersonFilterVM? filterVModel = (PersonFilterVM?) viewModel;

//                     //MarkFieldsAsRequired( personVModel, modelState );

//                     //ValidateCivilId( personVModel.CivilNumber, modelState );
//              }

//private void MarkFieldsAsRequired( PersonFilterVM? filterVModel, ModelStateDictionary modelState )
//{
//       var propertiesArray = filterVModel.GetType().GetProperties();

//       List<object> valuesList = new List<object>();

//       foreach ( var item in propertiesArray )
//       {
//              var name = item.Name;
//              var propValue = item.GetValue( filterVModel );
//              valuesList.Add( propValue );
//       }
//       //#######  I am here

//       bool allValuesAreNull = valuesList.All( x => x == null );

//       if ( !allValuesAreNull )
//       {
//              modelState.AddModelError( "FirstName", GetErrorMessage( "FirstName" ) );
//              modelState.AddModelError( "LastName", GetErrorMessage( "LastName" ) );
//              modelState.AddModelError( "GenderType", GetErrorMessage( "GenderType" ) );
//              modelState.AddModelError( "CivilNumber", GetErrorMessage( "CivilNumber" ) );
//       }
//}

//private void ValidateCivilId( string civilID, ModelStateDictionary modelState )
//{
//       this.FieldErrors.Clear();

//       //Validate Civil Id exists in DB
//       string? personCivilId = this.persons
//                                                   .Where( x => x.EGN.Equals( civilID ) )
//                                                   .Select( x => x.EGN )
//                                                   .FirstOrDefault();

//       if ( !string.IsNullOrEmpty( personCivilId ) )
//       {
//              string errorIsExists = string.Format( OutputMessages.ErrorValueExists, civilID );
//              this.FieldErrors.Add( errorIsExists );
//       }

//       if ( this.FieldErrors.Count > 0 )
//       {
//              this.AddModelStateError( nameof( civilID ) );
//       }
//}

//private string GetErrorMessage( string propertyName )
//{
//       return string.Format( OutputMessages.ErrorFieldIsRequired, propertyName );
//}
//       }
//}
