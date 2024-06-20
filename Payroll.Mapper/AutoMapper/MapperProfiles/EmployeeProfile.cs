using AutoMapper;
using Payroll.Models;
using Payroll.ModelsDto.EmployeeDtos;

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


			//CreateMap<Employee, GetEmployeeDto>()
			//	.ForMember( dest => dest.PersonDto,
			//		opt => opt.MapFrom( src => src.Person ) )
			//	.ForMember( dest => dest.IdCardPassport,
			//		opt => opt.MapFrom( src => src.Person.IdDocuments ) )
			//	.ForMember( dest => dest.ContractInfo,
			//		opt => opt.MapFrom( src => src.EmploymentContract ) )
			//	.ForMember( dest => dest.ContactInfo,
			//		opt => opt.MapFrom( src => src.Person.ContactInfoList.Last() )) ;

			//CreateMap<Person, PersonEmpDto>()
			//	.ForMember<string>( m => m.GenderType, 
			//		o => o.MapFrom( s => s.Gender.Type ) )
			//	.ReverseMap();

			//CreateMap<ContactInfo,ContactsEmpDto>();

			//CreateMap<IdDocument,IdDocumentEmpDto>()
			//	.ForMember(m => m.DocumentName,
			//		o => o.MapFrom(s => s.DocumentType.DocumentName));

			//CreateMap<EmploymentContract, ContractEmpDto>()
			//	.ForMember( m => m.DepartmentName,
			//		o => o.MapFrom( s => s.Department.Name ) );