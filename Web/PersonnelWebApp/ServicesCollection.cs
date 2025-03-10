﻿using System.Reflection;

using LegalFramework.Services.CalculationOfWorkExperience;
using LegalFramework.Services.DocumentGenerator;

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
using Payroll.ViewModels.EmpContractViewModels;
using Payroll.ViewModels.PersonViewModels;

using PersonnelWebApp.Utilities;

namespace PersonnelWebApp
{
    public static class ServicesCollection
	{
		public static void Collect(IServiceCollection services, IConfiguration Configuration)
		{
			string? connString = Configuration
				   .GetConnectionString("DefaultConnection");

			string? mapperAssembly = Configuration["AutoMapperAssembly"];

			services.AddDbContext<PayrollContext>(options => options.UseSqlServer(connString));

			services.AddAutoMapper(Assembly.Load(mapperAssembly));

			services.AddTransient<IMapEntity, MapEntity>();

			services.AddTransient<ICustomProjections, CustomProjections>();

			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

			services.AddTransient<IPrivateConfiguration, PrivateConfiguration>();

			//##################################################################

			services.AddScoped<ICompanyService, CompanyService>();

			services.AddTransient<IEmployeeService, EmployeeService>();

			services.AddTransient<IPersonService, PersonService>();

			services.AddTransient<IDiplomaService, DiplomaService>();

			services.AddTransient<IContactInfoService, ContactInfoService>();

			services.AddTransient<IAddressService, AddressService>();

			services.AddTransient<IDocumentsService, IdDocumentsService>();

			services.AddTransient<ILaborAgreementService, LaborAgreementService>();

			services.AddTransient<IAgreementTypeService, AgreementService>();

			services.AddTransient<ILaborCodeArticleService, LaborCodeArticleService>();

			services.AddTransient<IDepartmentService, DepartmentService>();

			//#################################################################################

			services.AddTransient(typeof(IFactorySortCollection<PersonVM>),
													 typeof(FactoryPersonsCollection));

			services.AddTransient(typeof(IFactorySortCollection<DiplomaVM>),
													 typeof(FactoryDiplomasCollection));

			services.AddTransient(typeof(IFactorySortCollection<ContactInfoVM>),
													 typeof(FactoryContactsCollection));

			services.AddTransient(typeof(IFactorySortCollection<AddressVM>),
													 typeof(FactoryAddressesCollection));

			services.AddTransient(typeof(IFactorySortCollection<IdDocumentVM>),
													 typeof(FactoryIdDocumentsCollection));

			services.AddTransient(typeof(IFactorySortCollection<LaborAgreementVM>),
													 typeof(FactoryLaborAgreements));


			//##############################################################################
			services.AddTransient<IValidate<ValidateBaseModel>, ValidateEmployeeVMService>();

			services.AddKeyedTransient<IValidate<ValidateBaseModel>, ValidateEmployeeVMService>
			("EmployeeValidate");

			services.AddKeyedTransient<IValidate<ValidateBaseModel>, ValidatePersonVMService>
			("PersonValidate");

			services.AddKeyedTransient<IValidate<ValidateBaseModel>, ValidateDiplomaVMService>
			("DiplomaValidate");

			services.AddKeyedTransient<IValidate<ValidateBaseModel>, ValidateAddressVMService>
			("AddressValidate");

			services.AddKeyedTransient<IValidate<ValidateBaseModel>, ValidateServiceStringValueExists>
			("StringValueExists");

			services.AddKeyedTransient<IValidate<ValidateBaseModel>, ValidateAgreementService>
			("AgreementValidate");

			//##############################################################################

			services.AddTransient<ICalculateExperience, CalculateExperience>();

			services.AddTransient<IViewModelLimitationsFactory, RestrictionsFactory>();

			services.AddTransient<ITempDocument, TempDocument>();

		}
	}
}
