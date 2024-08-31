using Payroll.Models;

namespace Payroll.Mapper.CustomMap.Test.CustomMapperReady
{
       public interface IProjectionTest
       {
              IQueryable<EmployeeBaseView> ProjectionTest( IQueryable<Employee> employees );
       }
}