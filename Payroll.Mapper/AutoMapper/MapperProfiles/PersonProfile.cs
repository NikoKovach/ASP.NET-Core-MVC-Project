using AutoMapper;
using Payroll.Models;
using Payroll.ModelsDto.EmployeeDtos.PersonDtos;

namespace Payroll.Mapper.AutoMapper.MapperProfiles
{
     public class PersonProfile : Profile
     {
          public PersonProfile()
          {
               CreateMap<Person, PersonDto>()
                    .ForMember<string>( m => m.GenderType, 
					o => o.MapFrom( s => s.Gender.Type ) )
               .ReverseMap();
               
          }
     }
}

//.ForMember( m => m.Gender.Type, o => o.MapFrom( s => s.GenderType ) );

               //CreateMap<Person, PersonIdDto>()
               //     .ForMember( m => m.GenderType, o => o.MapFrom( s => s.Gender.Type ) )
               //     .ReverseMap();