using Payroll.Data;
using Payroll.Mapper.CustomMap.Test;
using Payroll.Models;
using Payroll.ViewModels.EmployeeViewModels;

namespace TestPersonnel.Demo
{
       public static class Map
       {
              public static TResult? MapGenericTest<TSource, TResult>( Func<TSource, TResult> expression,
                                                                                                                      TSource source )
              {
                     var result = expression( source );

                     return result;
              }

              public static void UseDelegate( PayrollContext db )
              {
                     var dbSet = db.Employees;
                     var mapper = new AllEmployeeProfile();

                     //Func<Employee, AllEmployeeVM?> mapExpression = mapper.Map;
                     IQueryable<AllEmployeeVM>? result =
                            MapGenericTest<IQueryable<Employee>, IQueryable<AllEmployeeVM>?>
                           ( mapper.CreateProjection, dbSet );

                     Console.WriteLine( result.ToList().FirstOrDefault().Id );
              }
       }
}
