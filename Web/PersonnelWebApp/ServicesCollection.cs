using System.Reflection;

using LegalFramework.Services;
using Microsoft.EntityFrameworkCore;

using Payroll.Data;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Mapper.CustomMap;
using Payroll.Services.Services;
using Payroll.Services.Services.ServiceContracts;
using Payroll.Services.UtilitiesServices;
using Payroll.Services.UtilitiesServices.EntityValidateServices;
using Payroll.ViewModels;

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

                     //##################################################################
                     services.AddScoped<ICompanyService, CompanyService>();

                     services.AddTransient<IEmployeeService, EmployeeService>();

                     services.AddTransient<IPersonService, PersonService>();

                     services.AddTransient<IPersonsCollectionFactory, PersonsCollectionFactory>();

                     services.AddTransient<IDiplomaService, DiplomaService>();

                     services.AddTransient<IDiplomasCollectionFactory, DiplomasCollectionFactory>();

                     //##################################################################
                     services.AddTransient<ICalculateExperience, CalculateExperience>();

                     services.AddTransient<IViewModelLimitationsFactory, RestrictionsFactory>();

                     //##################################################################
                     services.AddTransient<IValidate<ValidateBaseModel>, ValidateEmployeeVMService>();

                     services.AddKeyedTransient<IValidate<ValidateBaseModel>, ValidateEmployeeVMService>
                                                                                                                                                   ( "EmployeeValidate" );

                     services.AddKeyedTransient<IValidate<ValidateBaseModel>, ValidatePersonVMService>
                                                                                                                                                    ( "PersonValidate" );

                     services.AddKeyedTransient<IValidate<ValidateBaseModel>, ValidateDiplomaVMService>
                                                                                                                                                    ( "DiplomaValidate" );

              }
       }
}
