using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Payroll.Services.UtilitiesServices.EntityValidateServices
{
       public interface IValidate<ValidateBaseModel>
       {
              public void Validate( ModelStateDictionary modelState, ValidateBaseModel viewModel );

              public void Validate( ModelStateDictionary modelState, IEnumerable<ValidateBaseModel> viewModelsCollection );
       }
}