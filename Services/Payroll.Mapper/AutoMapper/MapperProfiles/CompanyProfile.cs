﻿using AutoMapper;
using Payroll.Models;
using Payroll.ViewModels;

namespace Payroll.Mapper.AutoMapper.MapperProfiles
{
       public class CompanyProfile : Profile
       {
              public CompanyProfile()
              {
                     CreateMap<Company, CompanyVM>()
                            .ForMember( m => m.Employees, o => o.MapFrom( s => s.Employees ) )
                            .ReverseMap()
                            .ForMember( m => m.Employees, o => o.MapFrom( s => s.Employees ) );

                     CreateMap<Company, SearchCompanyVM>();
              }
       }
}
