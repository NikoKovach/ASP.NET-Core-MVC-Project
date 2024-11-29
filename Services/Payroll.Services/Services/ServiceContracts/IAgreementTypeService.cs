using Payroll.ViewModels.EmpContractViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IAgreementTypeService : IBasicAddUpdate<AgreementTypeVM>
       {
              IQueryable<AgreementTypeVM>? AllAgreements();

              IQueryable<AgreementTypeVM>? GetAgreementType( int? id );
       }
}