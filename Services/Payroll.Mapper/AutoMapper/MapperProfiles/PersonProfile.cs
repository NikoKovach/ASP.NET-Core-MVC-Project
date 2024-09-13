using AutoMapper;
using Payroll.Models;
using Payroll.ViewModels;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Mapper.AutoMapper.MapperProfiles
{
       public class PersonProfile : Profile
       {
              public PersonProfile()
              {
                     CreateMap<Person, PersonViewModel>()
                            .ForMember<string>( m => m.GenderType, o => o.MapFrom( s => s.Gender.Type ) )
                            .ReverseMap();

                     CreateMap<Person, SearchPersonVM>();
              }
       }
}
