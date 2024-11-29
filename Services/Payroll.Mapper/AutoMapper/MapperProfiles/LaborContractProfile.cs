using AutoMapper;
using Payroll.Models;
using Payroll.Models.EnumTables;
using Payroll.ViewModels.EmpContractViewModels;

namespace Payroll.Mapper.AutoMapper.MapperProfiles
{
       public class LaborContractProfile : Profile
       {
              public LaborContractProfile()
              {
                     CreateMap<EmploymentContract, LaborAgreementVM>()
                            .ForMember( m => m.ContractType, opt => opt.MapFrom( s => s.ContractType.Type ) )
                            .ForMember( m => m.LaborCodeArticle, opt => opt.MapFrom( s => s.LaborCodeArticle.Article ) )
                            .ForMember( m => m.DepartmentName, opt => opt.MapFrom( s => s.Department.Name ) )
                            .ForMember( m => m.FirstLastName, opt => opt.MapFrom( s => s.Employee.Person.FirstLastName ) )
                            .ForMember( m => m.FirstName, opt => opt.MapFrom( s => s.Employee.Person.FirstName ) )
                            .ForMember( m => m.LastName, opt => opt.MapFrom( s => s.Employee.Person.LastName ) )
                            .ForMember( m => m.CompanyId, opt => opt.MapFrom( s => s.Employee.CompanyId ) )
                     .ReverseMap()
                            .ForPath( m => m.ContractType.Type, opt => opt.MapFrom( s => s.ContractType ) )
                            .ForPath( m => m.LaborCodeArticle.Article, opt => opt.MapFrom( s => s.LaborCodeArticle ) )
                            .ForPath( m => m.Department.Name, opt => opt.MapFrom( s => s.DepartmentName ) );

                     CreateMap<ContractType, AgreementTypeVM>().ReverseMap();

                     CreateMap<LaborCodeArticle, LaborCodeArticleVM>().ReverseMap();
              }
       }
}
