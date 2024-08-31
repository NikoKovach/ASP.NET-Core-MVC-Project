using Payroll.Models;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Mapper.CustomMap
{
       public interface ICustomProjections
       {
              Dictionary<string, Func<IQueryable<Employee>, IQueryable<BaseEmployeeVM>>> EmployeeProjections
              { get; set; }
       }
}
