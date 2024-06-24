using AutoMapper;
using Payroll.Models;
using Payroll.ModelsDto.PersonViewModels;

namespace Payroll.Mapper.AutoMapper.MapperProfiles
{
    public class ContactInfoProfile : Profile
     {
          public ContactInfoProfile()
          {
               CreateMap<ContactInfo, ContactInfoDto>().ReverseMap();
          }
     }
}
