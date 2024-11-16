using Payroll.ViewModels.EmpContractViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface ILaborAgreementService
       {
              IQueryable<LaborAgreementVM>? All( int? companyId );

              IQueryable<LaborAgreementVM>? AllActive( int? companyId );

              IQueryable<LaborAgreementVM>? AllActive( int? companyId, string? sortParam, FilterAgreementVM? filter );
       }
}
