using Microsoft.Extensions.DependencyInjection;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Mapper.CustomMap;
using Payroll.ModelsDto;
using Payroll.Services.Services;
using Payroll.Services.Services.CompanyServices;
using Payroll.Services.Services.EmployeeServices;
using Payroll.Services.Services.ServiceContracts;

namespace PersonnelWebApp
{
	public static class ServicesCollection
	{
		public static void Collect(IServiceCollection services) 
		{ 
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

               services.AddScoped<ICompany, CompanyService>();

			services.AddTransient<IMapEntity,MapEntity>();

			services.AddTransient<IEmployee, EmployeeService>();

			services.AddTransient<IGetEmployeeMapping,GetEmployeeMapping>();
			
			services.AddTransient<IAllValidEntities<SearchCompanyDto>,
								ComponentSearchCompany>();
			

			//services.Configure<CompositeFileProviderOptions>();
		}
	}
}
