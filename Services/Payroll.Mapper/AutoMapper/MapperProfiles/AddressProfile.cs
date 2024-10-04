using AutoMapper;
using Payroll.Models;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Mapper.AutoMapper.MapperProfiles
{
       public class AddressProfile : Profile
       {
              public AddressProfile()
              {
                     CreateMap<Address, AddressVM>().ReverseMap();
              }
       }
}