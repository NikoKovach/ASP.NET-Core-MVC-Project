using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IDiplomaService : IBasicAddUpdate<DiplomaVM>, IBasicUpdateCollection<DiplomaVM>, IBasicDelete
       {
              IQueryable<DiplomaVM>? All( int? personId );

              IQueryable<DiplomaVM>? AllNotDeleted( int? personId, string? sortParam );

              IQueryable<string> TypesOfEducation();
       }
}