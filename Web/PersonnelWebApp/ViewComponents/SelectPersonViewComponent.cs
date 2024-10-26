using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PersonViewModels;

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
                     List<SelectListItem> personsList = new List<SelectListItem>();

                     List<SearchPersonVM>? persons = await this.service
                                                                .AllActive_SearchPersonVM()
                                                                .ToListAsync();

                     if ( persons != null && persons.Count > 0 )
                     {
                            foreach ( var person in persons )
                            {
                                   personsList.Add( new SelectListItem
                                   {
                                          Text = $"{person.FullName} -> Civil number : {person.CivilID}",
                                          Value = person.PersonId.ToString()
                                   } );
                            }
                     }

                     return View( personsList );
              }
       }
}
