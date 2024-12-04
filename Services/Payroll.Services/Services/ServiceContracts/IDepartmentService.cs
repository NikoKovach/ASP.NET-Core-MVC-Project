using Payroll.ViewModels.EmpContractViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IDepartmentService : IBasicAddUpdate<DepartmentVM>
       {
              IQueryable<DepartmentVM>? All();

              IQueryable<DepartmentVM>? GetEntity( int? entityId );
       }
}
