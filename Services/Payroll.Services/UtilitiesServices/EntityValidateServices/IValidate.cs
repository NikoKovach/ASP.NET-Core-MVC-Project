using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Payroll.Services.UtilitiesServices.EntityValidateServices
{
       public interface IValidate<ValidateBaseModel>
       {
              void Validate( ModelStateDictionary modelState, ValidateBaseModel viewModel,
                                   [CallerMemberName] string actionName = "", params object[] parameters );

              void Validate( ModelStateDictionary modelState, IEnumerable<ValidateBaseModel> viewModelsCollection );

              Task ValidateAsync( ModelStateDictionary modelState, ValidateBaseModel viewModel,
                                                [CallerMemberName] string actionName = "", params object[] parameters );
       }
}