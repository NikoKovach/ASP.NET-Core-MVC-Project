using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Payroll.Services.UtilitiesServices.EntityValidateServices
{
       public interface IValidate
       {
              public void Validate<TViewModel>( ModelStateDictionary modelState, TViewModel viewModel );
       }
}