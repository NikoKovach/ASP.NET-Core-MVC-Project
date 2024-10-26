using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Services.ServiceContracts;

namespace PersonnelWebApp.ViewComponents
{
       [ViewComponent( Name = "GenderTypes" )]
       public class GenderTypesViewComponent : ViewComponent
       {
              private readonly IPersonService service;

              public GenderTypesViewComponent( IPersonService personService )
              {
                     this.service = personService;
              }

              public async Task<IViewComponentResult> InvokeAsync()
              {
                     List<string> gendersList = await this.service.GenderTypes().ToListAsync();

                     return View( gendersList );
              }
       }
}
