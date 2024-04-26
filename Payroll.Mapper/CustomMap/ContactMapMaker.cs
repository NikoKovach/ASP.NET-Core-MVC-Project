//using Payroll.Data;
//using Payroll.Mapper.CustomMap.Contracts;
//using Payroll.Models;
//using Payroll.ModelsDto.EmployeeDtos.PersonDtos;

//namespace Payroll.Mapper.CustomMap
//{
//     public class ContactMapMaker : IMyMapper
//     {

//          public Func<PayrollContext, ContactInfo, ContactInfoDto> CreateMapWithContext = delegate (PayrollContext db, ContactInfo s)
//                      {
//                           return new ContactInfoDto { PhoneNumberOne = s.PhoneNumberOne };
//                      };

//          public Func<ContactInfo, ContactInfoDto> CreateMap = x =>
//               new ContactInfoDto
//               {
//                    E_MailAddress1 = x.E_MailAddress1
//               };

//          public TDestination SecondMap<TSource, TDestination>(Func<TSource, TDestination> func)
//          {

//               throw new NotImplementedException();
//          }
//     }
//}
