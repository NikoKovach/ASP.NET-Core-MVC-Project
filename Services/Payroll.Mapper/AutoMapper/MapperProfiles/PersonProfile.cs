using AutoMapper;
using Payroll.Models;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Mapper.AutoMapper.MapperProfiles
{
       public class PersonProfile : Profile
       {
              public PersonProfile()
              {
                     CreateMap<Person, PersonVM>()
                            .ForMember<string>( m => m.GenderType, opt => opt.MapFrom( s => s.Gender.Type ) )
                            .ForMember<string>( m => m.CivilNumber, opt => opt.MapFrom( s => s.EGN ) )
                            .ReverseMap();

                     CreateMap<Person, SearchPersonVM>();
              }
       }
}
