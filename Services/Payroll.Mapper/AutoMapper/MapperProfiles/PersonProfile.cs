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

                     CreateMap<Person, SearchPersonVM>()
                            .ForMember( m => m.PersonId, opt => opt.MapFrom( s => s.Id ) )
                            .ForMember( m => m.CivilID, opt => opt.MapFrom( s => s.EGN ) );

                     CreateMap<ContactInfo, ContactInfoVM>().ReverseMap();

                     CreateMap<Diploma, DiplomaVM>()
                           .ForMember( d => d.EducationTypeName, opt => opt.MapFrom( s => s.EducationType.Type ) )
                           .ReverseMap();

                     CreateMap<IdDocument, IdDocumentVM>()
                         .ForMember( m => m.DocumentName, opt => opt.MapFrom( s => s.DocumentType.DocumentName ) )
                         .ReverseMap();
              }
       }
}
