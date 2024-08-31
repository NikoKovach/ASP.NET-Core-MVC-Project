using Payroll.Models;

namespace Payroll.Mapper.CustomMap.Test.CustomMapperReady
{
       public class ProfileEmpBase : IProjectionTest
       {
              public IQueryable<EmployeeBaseView> ProjectionTest( IQueryable<Employee> employees )
              {
                     var empView = employees
                                                 .Select( x => new EmployeeBaseView
                                                 {
                                                        Id = x.Id,
                                                        CompanyId = x.CompanyId,
                                                        NumberFromTheList = x.NumberFromTheList,
                                                        IsPresent = x.IsPresent
                                                 } );

                     return empView;
              }
       }
}

