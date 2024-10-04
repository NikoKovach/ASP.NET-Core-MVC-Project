using AutoMapper;
using Payroll.Models;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Mapper.AutoMapper.MapperProfiles
{
       public class ContactInfoProfile : Profile
       {
              public ContactInfoProfile()
              {
                     CreateMap<ContactInfo, ContactInfoVM>().ReverseMap();
              }
       }
}
