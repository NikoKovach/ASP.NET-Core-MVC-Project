﻿using AutoMapper;
using Payroll.Models;
using Payroll.ModelsDto.PersonViewModels;

namespace Payroll.Mapper.AutoMapper.MapperProfiles
{
    public class PersonProfile : Profile
     {
          public PersonProfile()
          {
               CreateMap<Person, PersonDto>()
                    .ForMember<string>( m => m.GenderType, 
					o => o.MapFrom( s => s.Gender.Type ) )
               .ReverseMap();
               
          }
     }
}