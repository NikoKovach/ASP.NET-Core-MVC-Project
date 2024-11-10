using AutoMapper;
using Payroll.Mapper.AutoMapper.MapperProfiles;

namespace Payroll.Mapper.AutoMapper
{
       public class AutoMapperBuilder : IMapperCongiguration
       {
              public MapperConfiguration CreateMapperConfig()
              {
                     var mapperProfiles = new List<Profile>()
                     {
                            new CompanyProfile(),
                            new PersonProfile(),
                            new EmployeeProfile(),
                            new DiplomaProfile(),
                            new ContactInfoProfile(),
                            new AddressProfile(),
                            new IdDocumentProfile(),
                            new LaborContractProfile(),
                     };

                     var config = new MapperConfiguration( cfg => cfg.AddProfiles( mapperProfiles ) );

                     return config;
              }
       }
}
