using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IDiplomaService : IAddUpdate<DiplomaVM>, IDelete
       {
              IQueryable<DiplomaVM>? All( int? personId );

              IQueryable<DiplomaVM>? AllNotDeleted( int? personId, string? sortParam );

              IQueryable<string> TypesOfEducation();
       }
}