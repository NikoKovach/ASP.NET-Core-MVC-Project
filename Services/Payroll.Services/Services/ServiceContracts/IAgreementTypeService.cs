using Payroll.ViewModels.EmpContractViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IAgreementTypeService :
                                  IBasicAddUpdate<AgreementTypeVM>, IBasicGetEntityIQueryable<string?>
       {
              IQueryable<AgreementTypeVM>? AllAgreements();
       }
}