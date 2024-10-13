using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.CustomValidation
{
       //[AttributeUsage( AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false )]
       public class CannotBeNullAttribute : ValidationAttribute
       {
              private const string ErrorParamNullValue = "{0 } can't be null !";

              public CannotBeNullAttribute( string? propertyName )
              {
                     this.PropertyName = propertyName;
              }

              public string? PropertyName { get; }

              public override bool IsValid( object? value )
              {
                     int? intValue = Convert.ToInt32( value );

                     if ( intValue == null )
                     {
                            ErrorMessage = string.Format( ErrorParamNullValue, this.PropertyName );

                            return false;
                     }

                     return true;
              }


       }
}
