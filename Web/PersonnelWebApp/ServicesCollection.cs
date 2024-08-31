using System.Reflection;

using LegalFramework.Services;
using Microsoft.EntityFrameworkCore;

using Payroll.Data;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Mapper.CustomMap;
using Payroll.Services.Services;
using Payroll.Services.Services.CompanyServices;
using Payroll.Services.Services.EmployeeServices;
using Payroll.Services.Services.ServiceContracts;
using Payroll.Services.UtilitiesServices.EntityValidateServices;

namespace PersonnelWebApp
{
    public static class ServicesCollection
       {
              public static void Collect( IServiceCollection services, IConfiguration Configuration )
              {
                     string? connString = Configuration
                            .GetConnectionString( "DefaultConnection" );

                     string? mapperAssembly = Configuration[ "AutoMapperAssembly" ];

                     services.AddDbContext<PayrollContext>( options => options
                                           .UseSqlServer( connString ) );

                     services.AddAutoMapper( Assembly.Load( mapperAssembly ) );

                     services.AddTransient<IMapEntity, MapEntity>();

                     services.AddTransient<ICustomProjections, CustomProjections>();

                     services.AddScoped( typeof( IRepository<> ), typeof( Repository<> ) );

                     services.AddScoped<ICompany, CompanyService>();

                     services.AddTransient<IEmployee, EmployeeService>();

                     services.AddTransient<IPerson, PersonService>();

                     services.AddTransient<ICalculateExperience, CalculateExperience>();

                     services.AddTransient<IValidateEmployeeVModels, EmployeeVMValidate>();
              }
       }
}
