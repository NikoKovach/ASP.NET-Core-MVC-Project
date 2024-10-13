using System.ComponentModel.DataAnnotations;

namespace Payroll.ViewModels.CustomValidation
{
       public class DateIsEarlier : ValidationAttribute
       {
              public override bool IsValid( object? value )
              {
                     DateTime date = Convert.ToDateTime( value );

                     return date <= DateTime.UtcNow;
              }
       }
}
