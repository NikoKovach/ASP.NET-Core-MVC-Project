using LegalFramework.Services;
using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Mapper.CustomMap;
using Payroll.Services.Services.CompanyServices;
using Payroll.Services.Services.EmployeeServices;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels;

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
			
			services.AddTransient<IAllValidEntities<SearchCompanyViewModel>,
								SearchCompanyService>();

			services.AddTransient<IAllValidEntities<SearchPersonVM>,
								SearchPersonService>();

			services.AddTransient<ICalculateExperience,CalculateExperience>();

			services.AddTransient<IGetContractInfo,GetContractService>();
			
		}
	}
}
