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
		}
     }
}
