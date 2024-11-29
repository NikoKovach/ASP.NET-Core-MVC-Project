//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Payroll.Services.Services.ServiceContracts;
//using Payroll.ViewModels.EmployeeViewModels;

//namespace PersonnelWebApp.ViewComponents
//{
//       [ViewComponent( Name = "SelectEmployee" )]
//       public class SelectEmployeeViewComponent : ViewComponent
//       {
//              private readonly IEmployeeService service;

//              public SelectEmployeeViewComponent( IEmployeeService employeesService )
//              {
//                     this.service = employeesService;
//              }

//              public async Task<IViewComponentResult> InvokeAsync( string? companyId = null )
//              {
//                     List<SelectListItem> employeesList = new List<SelectListItem>();

//                     List<SearchEmployeeVM>? employees = new List<SearchEmployeeVM>();

//                     if ( string.IsNullOrEmpty( companyId ) )
//                     {
//                            bool parseSuccess = int.TryParse( companyId, out int resultCompanyId );

//                            if ( parseSuccess )
//                            {
//                                   employees = await this.service
//                                                                            .AllActive_SearchEmployeeVM( resultCompanyId )
//                                                                            .ToListAsync();
//                            }
//                     }

//                     if ( employees != null && employees.Count > 0 )
//                     {
//                            foreach ( var employee in employees )
//                            {
//                                   employeesList.Add( new SelectListItem
//                                   {
//                                          Text = $"{employee.EmployeeName} -> Single Civil No : {employee.CivilIdNumber}",
//                                          Value = employee.Id.ToString()
//                                   } );
//                            }
//                     }

//                     return View( employeesList );
//              }
//       }
//}
