using Microsoft.AspNetCore.Mvc;
using Payroll.Services.Services.ServiceContracts;

namespace PersonnelWebApp.ViewComponents
{
       [ViewComponent( Name = "PersonName" )]
       public class PersonNameViewComponent : ViewComponent
       {
              private readonly IPersonService service;

              public PersonNameViewComponent( IPersonService personService )
              {
                     this.service = personService;
              }

              public async Task<IViewComponentResult> InvokeAsync( int? personId )
              {
                     if ( personId == null || personId == 0 )
                     {
                            return View( "Default", string.Empty );
                     }

                     string? personName = await this.service.GetEntityNameAsync( personId );

                     if ( string.IsNullOrEmpty( personName ) )
                     {
                            return View( string.Empty );
                     }

                     return View( "Default", $"-{personName}" );
              }
       }
}
