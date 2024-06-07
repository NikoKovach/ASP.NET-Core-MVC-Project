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
               CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
          }
     }
}
