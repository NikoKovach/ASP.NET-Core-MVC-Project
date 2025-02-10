using AutoMapper;

using LegalFramework.Services.DocumentGenerator;
using LegalFramework.Services.Utilities.NumbersToWords;

using Microsoft.EntityFrameworkCore;

using Payroll.Data;
using Payroll.Mapper.CustomMap;
using Payroll.Models;
using Payroll.ViewModels;
using Payroll.ViewModels.EmpContractViewModels;
using Payroll.ViewModels.EmployeeViewModels;
using Payroll.ViewModels.PersonViewModels;

namespace TestPersonnel.Demo
{
	public static class DocGeneratorDemo
	{
		public static void FirstPdf(PayrollContext context, IMapper autoMapper)
		{
			NumbersInWordsDemo();

			var tempDoc = new TempDocument();
			string path = @"D:\SoftwareCourses\A Exercises\AA Git Projects\ASP.NET-Core-MVC-Payroll Web Project\Web\PersonnelWebApp\AppFolder\Temp";

			//int? companyId, int? agreementId
			int companyId = 5;
			int agreementId = 2;

			LaborAgreementVM contractModel = GetContractVM(context, autoMapper, companyId, agreementId);

			AddressEmpVM? conclusionAddressVM = GetAddressOfRegistration(context, autoMapper,
																		 companyId, agreementId);

			CompanyVM? company = CompanyViewModel(context, autoMapper, companyId);

			AddressEmpVM? companyHeadquartersVM = GetCompanyHeadquarters(context, autoMapper, companyId);

			PersonInfoContractVM employee = EmployeeViewModel(context);

			object[] docModels =
			[
				contractModel,
				conclusionAddressVM,
				company,
				companyHeadquartersVM,
				employee
			];

			var filePath = tempDoc.CreateFile(path, "laborContract", docModels);

			//PdfGenerator.Create();

			Console.WriteLine();
		}

		private static void NumbersInWordsDemo()
		{
			IConvertingNumberToWords convertor = new ConvertingNumberToWordsBG();

			var result = convertor.WriteNumberInWords(20.25m);

			Console.WriteLine(result);
		}

		private static LaborAgreementVM GetContractVM(PayrollContext context, IMapper mapper,
													  int companyId, int agreementId)
		{
			var contract = context.EmploymentContracts
												  .AsNoTracking()
												  .Where(x => x.Id == agreementId);

			//LaborAgreementVM? contractVM = mapper.Map<EmploymentContract, LaborAgreementVM>(contract);

			var contractVM = mapper.ProjectTo<LaborAgreementVM>(contract).FirstOrDefault();

			return contractVM;
		}

		private static AddressEmpVM? GetAddressOfRegistration(PayrollContext context, IMapper mapper,
																	int companyId, int agreementId)
		{
			Address? address = context.EmploymentContracts
												.AsNoTracking()
												.Where(x => x.Id == agreementId)
												.Select(x => x.PlaceOfRegistration)
												.FirstOrDefault();

			AddressEmpVM? conclusionAddressVM = mapper.Map<Address, AddressEmpVM>(address);

			return conclusionAddressVM;
		}

		private static CompanyVM? CompanyViewModel(PayrollContext context, IMapper autoMapper, int companyId)
		{
			Company? company = context.Companies.FirstOrDefault(x => x.Id == companyId);

			var viewModel = autoMapper.Map<CompanyVM>(company);

			return viewModel;
		}

		private static AddressEmpVM? GetCompanyHeadquarters(PayrollContext context, IMapper mapper,
																					int companyId)
		{
			Address? address = context.Companies
									  .AsNoTracking()
									  .Where(x => x.Id == companyId)
									  .Select(x => x.HeadquartersAddress)
									  .FirstOrDefault();

			AddressEmpVM? headquartersVM = mapper.Map<Address, AddressEmpVM>(address);

			return headquartersVM;
		}

		private static PersonInfoContractVM EmployeeViewModel(PayrollContext context)
		{
			var customMap = new ProfilePersonInfoContractVM();

			var employees = context.Employees
								   .Where(x => x.CompanyId == 5 && x.Id == 1)
								   .AsNoTracking();

			var empVM = customMap.Projection(employees).FirstOrDefault();

			return empVM;

		}
	}
}

//LaborAgreementVM contractModel = new LaborAgreementVM
//{
//	ContractNumber = "123456",
//	ContractDate = DateTime.Now,

//};

//AddressEmpVM? conclusionAddressVM = new AddressEmpVM
//{
//	//Region = "",
//	//Municipality = "",
//	City = "Казанлък"
//};

//CompanyVM? company = new CompanyVM
//{
//	Name = "Test",
//	UniqueIdentifier = "789456123",
//	RepresentedBy = "Ceo WWW",
//	RepresentativeIdNumber = "2323232323"
//};