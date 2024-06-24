using AutoMapper;
using Payroll.Models;
using Payroll.ModelsDto.PersonViewModels;

namespace Payroll.Mapper.AutoMapper.MapperProfiles
{
    public class IdDocumentProfile : Profile
     {
          public IdDocumentProfile()
          {
               CreateMap<IdDocument, IdDocumentDto>()
                    .ForPath( m => m.DocumentName,
                                   opt => opt.MapFrom( s => s.DocumentType.DocumentName )
                              )
                    .ReverseMap();
          }
     }
}

/*
 .ForMember( m => m.DocumentTypeId,
                                   opt => opt.MapFrom( s => s.DocumentType.Id )
                              )
 .ForMember( m => m.DocumentType,
                                   opt => opt.MapFrom( s => s.DocumentType )
                              )
 */