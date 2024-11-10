using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels;

namespace PersonnelWebApp.ViewComponents
{
       [ViewComponent( Name = "SelectCompany" )]
       public class SelectCompanyViewComponent : ViewComponent
       {
              private readonly ICompanyService service;

              public SelectCompanyViewComponent( ICompanyService companyService )
              {
                     this.service = companyService;
              }

              public async Task<IViewComponentResult> InvokeAsync( string? parentView )
              {
                     CompanyListViewModel? companyList = new CompanyListViewModel();

                     List<SearchCompanyVM>? companies = await this.service.AllActive_SearchCompanyVM()
                                                                                                                            .ToListAsync();

                     foreach ( var company in companies )
                     {
                            companyList.Companies.Add( new SelectListItem
                            {
                                   Text = $"{company.Info}",
                                   Value = company.Id.ToString()
                            } );
                     }

                     companyList.Companies = companyList.Companies.OrderBy( x => x.Text ).ToList();

                     if ( !string.IsNullOrEmpty( parentView ) )
                     {
                            if ( parentView.Equals( "Index" ) )
                            {
                                   return View( "IndexEmployees", companyList );
                            }
                            if ( parentView.Equals( "IndexContracts" ) )
                            {
                                   return View( parentView, companyList );
                            }
                     }

                     return View( companyList );
              }
       }
}
