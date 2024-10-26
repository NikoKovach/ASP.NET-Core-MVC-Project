using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IContactInfoService : IAddUpdate<ContactInfoVM>, IDelete
       {
              IQueryable<ContactInfoVM>? All( int? personId );

              IQueryable<ContactInfoVM>? AllNotDeleted( int? personId, string? sortParam );
       }
}