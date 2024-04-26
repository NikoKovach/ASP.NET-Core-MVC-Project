using AutoMapper;
using Payroll.Models;
using Payroll.ModelsDto.EmployeeDtos.PersonDtos;

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
