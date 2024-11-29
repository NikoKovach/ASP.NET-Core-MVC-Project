using AutoMapper;
using Payroll.Models;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Mapper.AutoMapper.MapperProfiles
{
       public class EmployeeProfile : Profile
       {
              public EmployeeProfile()
              {
                     CreateMap<Employee, EmployeeVM>().ReverseMap();

                     CreateMap<Employee, SearchEmployeeVM>()
                            .ForMember( m => m.EmployeeName, opt => opt.MapFrom( s => s.Person.FirstLastName ) )
                            .ForMember( m => m.CivilIdNumber, opt => opt.MapFrom( s => s.Person.EGN ) );
              }
       }
}
