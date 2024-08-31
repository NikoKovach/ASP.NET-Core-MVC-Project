using AutoMapper;
using Payroll.Models;
using Payroll.ModelsDto.PersonViewModels;

namespace Payroll.Mapper.AutoMapper.MapperProfiles
{
    public class AddressProfile : Profile
     {
          public AddressProfile()
          {
               CreateMap<Address, AddressDto>().ReverseMap();
          }
     }
}