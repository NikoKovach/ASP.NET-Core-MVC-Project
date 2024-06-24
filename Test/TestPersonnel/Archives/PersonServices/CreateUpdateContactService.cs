using AutoMapper;
using Payroll.Data;
using Payroll.Models;
using Payroll.ModelsDto.PersonViewModels;

namespace Payroll.Services.Services.EmployeeServices.PersonServices
{
    public class CreateUpdateContactService : CreateUpdateEntityService<ContactInfoDto, ContactInfo>
     {
          public CreateUpdateContactService(PayrollContext payrollContext, IMapper autoMapper) : base(payrollContext, autoMapper)
          {
          }

          public override bool AddRecord(ContactInfo entity)
          {
               try
               {
                    Context.ContactInfos.Add(entity);

                    Context.SaveChanges();

                    return true;
               }
               catch (Exception)
               {
                    return false;
               }
          }

          public override ContactInfo CreateObject(ContactInfoDto view)
          {
               var contact = new ContactInfo
               {
                    PhoneNumberOne = view.PhoneNumberOne,
                    PhoneNumberTwo = view.PhoneNumberTwo,
                    E_MailAddress1 = view.E_MailAddress1,
                    E_MailAddress2 = view.E_MailAddress2,
                    WebSite = view.WebSite,
                    PersonId = view.PersonId,
                    HasBeenDeleted = false,
               };

               return contact;
          }
     }
}
