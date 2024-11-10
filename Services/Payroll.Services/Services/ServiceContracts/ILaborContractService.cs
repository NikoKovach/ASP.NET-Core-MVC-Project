using Payroll.ViewModels.LaborContractViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface ILaborContractService
       {
              IQueryable<LaborContractVM>? All( int? companyId );

              IQueryable<LaborContractVM>? AllActive( int? companyId );
       }
}
