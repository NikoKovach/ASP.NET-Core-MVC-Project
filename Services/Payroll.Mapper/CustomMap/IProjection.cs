using Payroll.Models;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Mapper.CustomMap
{
       public interface IProjection
       {
              IQueryable<BaseEmployeeVM> Projection( IQueryable<Employee> employees );
       }
}