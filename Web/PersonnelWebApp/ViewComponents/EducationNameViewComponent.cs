using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Services.ServiceContracts;

namespace PersonnelWebApp.ViewComponents
{
       [ViewComponent( Name = "EducationName" )]
       public class EducationNameViewComponent : ViewComponent
       {
              private readonly IDiplomaService service;

              public EducationNameViewComponent( IDiplomaService diplomaService )
              {
                     this.service = diplomaService;
              }

              public async Task<IViewComponentResult> InvokeAsync()
              {
                     List<string>? educationList = await this.service.TypesOfEducation().ToListAsync();

                     return View( educationList );
              }
       }
}
