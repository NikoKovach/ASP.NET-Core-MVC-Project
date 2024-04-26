using AutoMapper;
using Payroll.Models;
using Payroll.ModelsDto.EmployeeDtos.PersonDtos;

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

/*
 * 
  //.ForMember( d => d.PersonId, opt =>
  //opt.MapFrom( s => s.PersonCurrentAddresesses
                      
  //           ) )

.ForMember( d => d.EmployeeId,
   opt => opt.MapFrom( s => s.PersonPermanentAddresses
   .Select( x => x.Employee.Id ) ) )
 */