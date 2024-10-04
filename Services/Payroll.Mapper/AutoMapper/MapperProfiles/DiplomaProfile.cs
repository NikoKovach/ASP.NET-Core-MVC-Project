using AutoMapper;
using Payroll.Models;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Mapper.AutoMapper.MapperProfiles
{
       public class DiplomaProfile : Profile
       {
              public DiplomaProfile()
              {
                     CreateMap<Diploma, DiplomaVM>()
                            .ForMember( d => d.EducationTypeName, opt => opt.MapFrom( s => s.EducationType.Type ) )
                            .ReverseMap();
              }
       }
}
