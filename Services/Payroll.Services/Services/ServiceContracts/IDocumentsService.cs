using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IDocumentsService : IBasicAddUpdate<IdDocumentVM>,
                                                                         IBasicUpdateCollection<IdDocumentVM>, IBasicDelete
       {
              IQueryable<IdDocumentVM>? All( int? personId );

              IQueryable<IdDocumentVM>? AllNotDeleted( int? personId, string? sortParam );

              IQueryable<string>? IdDocumentTypes();
       }
}
