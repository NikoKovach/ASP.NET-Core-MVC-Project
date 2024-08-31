using Payroll.Models;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Mapper.CustomMap.Test
{
       public interface IGetEmployeeMapping
       {
              public IQueryable<GetEmployeeVM> CustomMap
                     ( IQueryable<Employee>? employees );
       }
}