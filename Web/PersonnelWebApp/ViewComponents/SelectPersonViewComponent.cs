using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Services.ServiceContracts;
using PersonnelWebApp.Models;

namespace PersonnelWebApp.ViewComponents
{
       [ViewComponent( Name = "SelectPerson" )]
       public class SelectPersonViewComponent : ViewComponent
       {
              private readonly IPersonService service;

              public SelectPersonViewComponent( IPersonService personService )
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
                                   Value = item.PersonId.ToString()
                            } );
                     }

                     return View( personList );
              }
       }
}
