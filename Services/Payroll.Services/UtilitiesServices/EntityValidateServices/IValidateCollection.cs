using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Payroll.Services.UtilitiesServices.EntityValidateServices
{
       public interface IValidateCollection<ValidateBaseModel>
       {
              public void Validate( ModelStateDictionary modelState, IEnumerable<ValidateBaseModel> viewModelsCollection );
       }
}