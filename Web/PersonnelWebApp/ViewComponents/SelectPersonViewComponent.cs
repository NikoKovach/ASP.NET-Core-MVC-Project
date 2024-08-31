using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Services.CompanyServices;
using PersonnelWebApp.Models;

namespace PersonnelWebApp.ViewComponents
{
       [ViewComponent( Name = "SelectPerson" )]
       public class SelectPersonViewComponent : ViewComponent
       {
              private readonly IPerson service;

              public SelectPersonViewComponent( IPerson personService )
              {
                     this.service = personService;
              }

              public async Task<IViewComponentResult> InvokeAsync()
              {
                     var personList = new PersonListViewModel();

                     var persons = await this.service
                                                               .AllActive_SearchPersonVM()
                                                               .ToListAsync();

                     foreach ( var item in persons )
                     {
                            personList.Persons.Add( new SelectListItem()
                            {
                                   Text = item.ToString(),
                                   Value = item.Id.ToString()
                            } );
                     }

                     return View( personList );
              }
       }
}
