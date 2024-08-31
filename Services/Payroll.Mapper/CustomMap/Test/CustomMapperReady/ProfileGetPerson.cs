using Payroll.Models;

namespace Payroll.Mapper.CustomMap.Test.CustomMapperReady
{
       public class ProfileGetPerson : IProjectionTest
       {
              public IQueryable<EmployeeBaseView> ProjectionTest( IQueryable<Employee> employees )
              {
                     var empView = employees
                                                 .Select( x => new GetEmpPersonView
                                                 {
                                                        Id = x.Id,
                                                        CompanyId = x.CompanyId,
                                                        NumberFromTheList = x.NumberFromTheList,
                                                        IsPresent = x.IsPresent,
                                                        PersonId = x.Person.Id,
                                                        FullName = $"{x.Person.FirstName} {x.Person.MiddleName} {x.Person.LastName}",
                                                        EGN = x.Person.EGN,
                                                        GenderType = x.Person.Gender.Type
                                                 } );

                     return empView;
              }
       }
}
