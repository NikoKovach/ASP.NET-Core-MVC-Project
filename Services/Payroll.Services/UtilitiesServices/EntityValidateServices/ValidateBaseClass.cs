﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Payroll.ViewModels;

namespace Payroll.Services.UtilitiesServices.EntityValidateServices
{
       public abstract class ValidateBaseClass : IValidate<ValidateBaseModel>
       {
              protected string newRow = "\r\n";

              public ValidateBaseClass()
              {
                     this.FieldErrors = new List<string>();
              }

              protected ModelStateDictionary ModelState { get; set; }

              protected List<string> FieldErrors { get; set; }

              public abstract void Validate( ModelStateDictionary modelState, ValidateBaseModel viewModel );

              public virtual void Validate( ModelStateDictionary modelState, IEnumerable<ValidateBaseModel> viewModelsCollection )
              {
                     return;
              }

              protected void AddModelStateError( string propName, string? keyString = null )
              {
                     string generalError = GenerateErrorString( this.FieldErrors, propName );

                     this.ModelState.AddModelError( keyString, generalError );
              }

              protected string GenerateErrorString( List<string> fieldErrors, string propName )
              {
                     StringBuilder sb = new StringBuilder();

                     sb.Append( $"{propName} :{newRow}" );

                     for ( int i = 0; i < fieldErrors.Count; i++ )
                     {
                            if ( i == fieldErrors.Count - 1 )
                            {
                                   sb.Append( $"{i + 1}.{fieldErrors[ i ]}" );
                                   break;
                            }

                            sb.Append( $"{i + 1}.{fieldErrors[ i ]}{newRow}" );
                     }

                     return sb.ToString();
              }

              protected string GetDisplayName( PropertyInfo? property )
              {
                     CustomAttributeData? attributeData = property
                                                                                         .GetCustomAttributesData()
                                                                                         .FirstOrDefault( x =>
                                                                                         x.AttributeType == typeof( DisplayAttribute ) );
                     if ( attributeData == null )
                     {
                            return property.Name;
                     }

                     string? displayName = attributeData.NamedArguments
                                                                                     .FirstOrDefault()
                                                                                     .TypedValue.Value.ToString();

                     return displayName;
              }
              //###################################################
              protected string GetModelStateKeyString( string rootName, string firstLevelChild )
              {
                     return $"{rootName}.{firstLevelChild}";
              }

              protected string GetModelStateKeyString( string rootName, int index, string firstLevelChild )
              {
                     return $"{rootName}[{index}].{firstLevelChild}";
              }
       }
}
