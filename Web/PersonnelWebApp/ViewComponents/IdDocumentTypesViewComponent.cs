using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Services.Services.ServiceContracts;

namespace PersonnelWebApp.ViewComponents
{
       [ViewComponent( Name = "IdDocumentTypes" )]
       public class IdDocumentTypesViewComponent : ViewComponent
       {
              private readonly IDocumentsService service;

              public IdDocumentTypesViewComponent( IDocumentsService documentsService )
              {
                     this.service = documentsService;
              }

              public async Task<IViewComponentResult> InvokeAsync()
              {
                     List<string> documentTypesList = await this.service.IdDocumentTypes().ToListAsync();

                     return View( documentTypesList );
              }
       }
}
