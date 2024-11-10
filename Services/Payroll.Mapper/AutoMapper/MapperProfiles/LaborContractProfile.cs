using AutoMapper;
using Payroll.Models;
using Payroll.ViewModels.LaborContractViewModels;

namespace Payroll.Mapper.AutoMapper.MapperProfiles
{
       public class LaborContractProfile : Profile
       {
              public LaborContractProfile()
              {
                     // TODO :
                     CreateMap<EmploymentContract, LaborContractVM>()
                            .ReverseMap();
                     //.ForMember( m => m.Employees, o => o.MapFrom( s => s.Employees ) )
                     //.ForMember( m => m.Employees, o => o.MapFrom( s => s.Employees ) );

              }
       }
}
