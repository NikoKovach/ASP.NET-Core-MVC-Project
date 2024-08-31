using Payroll.Data.Common;
using Payroll.Models;

namespace TestPersonnel.Demo.Services
{
       public class EntitiesTastClass : IAllEntitiesTest
       {
              private IRepository<Employee> repository;

              public EntitiesTastClass( IRepository<Employee> empRepository )
              {
                     this.repository = empRepository;
              }

              public IQueryable<Employee> All( int companyId )
              {
                     IQueryable<Employee>? empList = this.repository
                                                                                   .AllAsNoTracking()
                                                                                    .Where( x => x.CompanyId == companyId && x.IsPresent == true );

                     return empList;
              }

              public IQueryable<Employee> GetAll<Employee>( int parameter )
              {
                     throw new NotImplementedException();
              }

              public IQueryable<Employee> GetAllActive<Employee>( int parameter )
              {
                     throw new NotImplementedException();
              }
       }
}
