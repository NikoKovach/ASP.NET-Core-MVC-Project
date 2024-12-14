using Payroll.ViewModels.EmpContractViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IDepartmentService : IBasicAddUpdate<DepartmentVM>, IBasicGetEntityIQueryable<string?>
       {
              IQueryable<DepartmentVM>? All();

              //IQueryable<DepartmentVM>? GetEntity( int? entityId );
       }
}
