using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
       public interface IContactInfoService : IBasicAddUpdate<ContactInfoVM>,
                                                                          IBasicUpdateCollection<ContactInfoVM>, IBasicDelete
       {
              IQueryable<ContactInfoVM>? All( int? personId );

              IQueryable<ContactInfoVM>? AllNotDeleted( int? personId, string? sortParam );
       }
}