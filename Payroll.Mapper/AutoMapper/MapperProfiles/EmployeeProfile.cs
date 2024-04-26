using AutoMapper;
using Payroll.Models;
using Payroll.ModelsDto.EmployeeDtos;
using Payroll.ModelsDto.EmployeeDtos.PersonDtos;

namespace Payroll.Mapper.AutoMapper.MapperProfiles
{
     public class EmployeeProfile : Profile
     {
          public EmployeeProfile()
          {
               CreateMap<Employee, EmployeeDto>().ReverseMap();
               CreateMap<Person, PersonDto>()
               .ForMember<string>(m => m.GenderType,o => o.MapFrom(s => s.Gender.Type))
               .ReverseMap();
          }
     }
}
