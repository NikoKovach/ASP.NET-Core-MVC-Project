using Payroll.Models;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Mapper.CustomMap
{
       public class CustomProjections : ICustomProjections
       {
              public CustomProjections()
              {
                     this.SetEmployeeProjections();
              }

              public Dictionary<string, Func<IQueryable<Employee>, IQueryable<BaseEmployeeVM>>> EmployeeProjections { get; set; }

              private void SetEmployeeProjections()
              {
                     this.EmployeeProjections =
                            new Dictionary<string, Func<IQueryable<Employee>, IQueryable<BaseEmployeeVM>>>()
                            {
                                   { "AllEmployees",new ProfileAllEmployeeVM().Projection },

                                   { "GetEmployeeVM",new ProfileGetEmployeeVM().Projection }
                            };
              }
       }
}

//Dictionary<string,Func<IQueryable<Employee>, IQueryable<EmployeeBaseView>>>
//{
//              {"BaseProjection", baseProjection.ProjectionTest},

//              { "PersonProjection",personProjection.ProjectionTest },

//              { "ContractProjection",contractProjection.ProjectionTest }
//};
