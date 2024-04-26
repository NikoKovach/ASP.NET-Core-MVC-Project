using AutoMapper;
using Payroll.Models;
using Payroll.ModelsDto;

namespace Payroll.Mapper.AutoMapper.MapperProfiles
{
     public class CompanyProfile : Profile
     {
          public CompanyProfile()
          {
               CreateMap<Company, ModelsDto.CompanyDto>()
                    .ForMember(m => m.Employees,o => o.MapFrom(s => s.Employees))
                    .ReverseMap()
                    .ForMember(m => m.Employees,o => o.MapFrom(s => s.Employees));
          }
     }
}
