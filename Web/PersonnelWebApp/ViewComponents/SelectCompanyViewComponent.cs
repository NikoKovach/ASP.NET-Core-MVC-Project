using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Services.ServiceContracts;
using PersonnelWebApp.Models;

namespace PersonnelWebApp.ViewComponents
{
       [ViewComponent( Name = "SelectCompany" )]
       public class SelectCompanyViewComponent : ViewComponent
       {
              private readonly ICompany service;

              public SelectCompanyViewComponent( ICompany companyService )
              {
                     this.service = companyService;
              }

              public async Task<IViewComponentResult> InvokeAsync()
              {
                     var companyList = new CompanyListViewModel();

                     var companies = await this.service
                                                                     .AllActive_SearchCompanyVM()
                                                                     .ToListAsync();

                     foreach ( var item in companies )
                     {
                            companyList.Companies.Add( new SelectListItem()
                            {
                                   Text = $"{item.Info}",
                                   Value = item.Id.ToString()
                            }
                            );
                     }

                     return View( companyList );
              }
       }
}
