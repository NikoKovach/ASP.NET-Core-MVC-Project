using Payroll.ViewModels.ModelRestrictions;

namespace Payroll.Services.UtilitiesServices.EntityValidateServices
{
       public interface IViewModelLimitationsFactory
       {
              public Dictionary<string, ViewModelRestrictions> Limitations { get; }

       }
}