using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Payroll.Services.UtilitiesServices.EntityValidateServices
{
       public interface IValidate<ValidateBaseModel>
       {
              public void Validate( ModelStateDictionary modelState, ValidateBaseModel viewModel,
                                   [CallerMemberName] string actionName = "", params object[] parameters );

              public void Validate( ModelStateDictionary modelState, IEnumerable<ValidateBaseModel> viewModelsCollection );
       }
}