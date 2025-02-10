using LegalFramework.Services.DocumentGenerator;

using Microsoft.EntityFrameworkCore;

using Payroll.Data.Common;
using Payroll.Mapper.AutoMapper;
using Payroll.Models;
using Payroll.Services.Services.ServiceContracts;
using Payroll.Services.Utilities;
using Payroll.ViewModels.EmpContractViewModels;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Services.Services
{
	public class LaborAgreementService : ILaborAgreementService
	{
		private IRepository<EmploymentContract> repository;
		private IMapEntity mapper;
		private IFactorySortCollection<LaborAgreementVM> agreementsFactory;
		private ITempDocument documentBuilder;

		public LaborAgreementService(IRepository<EmploymentContract> empRepository,
									 IMapEntity mapper,
									 IFactorySortCollection<LaborAgreementVM> contractsFactory,
									 ITempDocument tempDocumentBuilder)
		{
			this.repository = empRepository;

			this.mapper = mapper;

			this.agreementsFactory = contractsFactory;

			this.documentBuilder = tempDocumentBuilder;
		}

		public IQueryable<LaborAgreementVM>? All(int? companyId)
		{
			var contracts = this.repository
								.Context.Employees
								.AsNoTracking()
								.Where(x => x.CompanyId == companyId
										&& x.EmploymentContract.HasBeenDeleted == false
										&& x.EmploymentContract.ContractType.Type.Equals("Employment Contract"))
								.Select<Employee, EmploymentContract>(x => x.EmploymentContract);

			var empContractsVM = this.mapper.ProjectTo<EmploymentContract, LaborAgreementVM>(contracts);

			return empContractsVM;
		}

		public IQueryable<LaborAgreementVM>? AllActive(int? companyId)
		{
			IQueryable<LaborAgreementVM>? empContractsVM = this.All(companyId)
															   .Where(x => x.IsActive == true);

			return empContractsVM;
		}

		public IQueryable<LaborAgreementVM>? AllActive(int? companyId, string? sortParam,
													   FilterAgreementVM? filter)
		{
			IQueryable<LaborAgreementVM>? activeAgreements = this.AllActive(companyId);

			if (companyId != null && companyId > 0 || !string.IsNullOrEmpty(sortParam))
			{
				IQueryable<LaborAgreementVM>? resultAgreements =
				this.agreementsFactory.SortedCollection(sortParam, filter, activeAgreements);

				return resultAgreements;
			}

			return activeAgreements;
		}

		public IQueryable<LaborAgreementVM>? GetContract(int? contractId, int? companyId)
		{
			IQueryable<LaborAgreementVM>? agreement = this.AllActive(companyId)
														  .Where(x => x.Id == contractId);

			return agreement;
		}

		public async Task AddAsync(LaborAgreementVM agreementVM)
		{
			EmploymentContract? contract = this.mapper.Map<LaborAgreementVM, EmploymentContract>(agreementVM);

			if (contract != null)
			{
				await this.repository.AddAsync(contract);

				await this.repository.SaveChangesAsync();
			}
		}

		public async Task UpdateAsync(LaborAgreementVM agreementVM)
		{
			EmploymentContract? contract = this.mapper.Map<LaborAgreementVM, EmploymentContract>(agreementVM);

			if (contract != null)
			{
				this.repository.Update(contract);

				await repository.SaveChangesAsync();
			}
		}

		public async Task<string?> CreateTempFileAsync(string appFolderPath, string? relativeFolderName,
											  int? companyId, int? agreementId, string? fileTypeVersion)
		{
			//fileTypeVersion = bul-pdf ; eng-pdf ; bul-rtf ; eng-rtf
			IDictionary<string, string> docAttributes = ParseFileVersion(fileTypeVersion);

			object[]? documentModels = await CollectDocumentModelsAsync(companyId, agreementId);

			string tempDirPath = BuildTempFilePath(appFolderPath);


			try
			{
				string? tempFileFullPath = this.documentBuilder
										   .CreateFile(tempDirPath, docAttributes["documentType"],
													   documentModels, docAttributes["fileType"],
													   docAttributes["language"]);

				//string tempFileFullPath = tempDirPath;

				string? filePath = EnvironmentService.CreateRelativePath(tempFileFullPath,
																		 relativeFolderName, appFolderPath);

				return filePath;
			}
			catch (FileNotFoundException fileEx)
			{
				return $"{fileEx.Message}\r\nInner Exception : {fileEx.InnerException}";
			}
			catch (InvalidOperationException operationEx)
			{
				return $"{operationEx.Message}\r\nInner Exception : {operationEx.InnerException}";
			}
			catch (Exception ex)
			{
				return $"{ex.Message}";
			}
		}

		//##############################################################

		private async Task<object[]?> CollectDocumentModelsAsync(int? companyId, int? agreementId)
		{
			LaborAgreementVM? contractModel = await this.GetContract(agreementId, companyId)
														.FirstOrDefaultAsync();

			Address? contractConclusionAddress = await this.repository
													   .AllAsNoTracking()
													   .Where(x => x.Id == agreementId)
													   .Select(x => x.PlaceOfRegistration)
													   .FirstOrDefaultAsync();

			AddressEmpVM? conclusionAddressVM =
				this.mapper.Map<Address, AddressEmpVM>(contractConclusionAddress);

			//Olso : Company info - any companyViewModel
			//Employee ino - any employeeViewModel

			List<object> documentModels = new List<object>
			{
				contractModel,conclusionAddressVM,
			};

			return documentModels.ToArray();
		}

		private IDictionary<string, string> ParseFileVersion(string? fileTypeVersion)
		{
			//fileTypeVersion = bul-pdf ; eng-pdf ; bul-rtf ; eng-rtf
			/*
			 string? documentType,string fileType = "pdf", string language = "bul"
			 */
			//fileTypeVersion = fileTypeVersion ?? "bul-pdf";

			string languageExtension = (string.IsNullOrEmpty(fileTypeVersion)) ? "bul-pdf" : fileTypeVersion;

			string[] fileAttributesArr = languageExtension.Split('-');

			IDictionary<string, string> fileTypeAttributes = new Dictionary<string, string>
			{
				{ "documentType" , "laborContract"},
				{ "fileType" , fileAttributesArr[1]},
				{ "language" , fileAttributesArr[0]},
			};

			return fileTypeAttributes;
		}

		private string BuildTempFilePath(string appFolderPath)
		{
			string tempFolderName = "Temp";

			string tempDirPath = EnvironmentService.GetPath(appFolderPath, tempFolderName) ?? string.Empty;

			if (!string.IsNullOrEmpty(tempDirPath) && !Directory.Exists(tempDirPath))
			{
				tempDirPath = EnvironmentService.CreateDir(appFolderPath, tempFolderName) ?? string.Empty;
			}

			return tempDirPath;
		}
	}
}


//appFolderPath : "AppFolder",
//relativeFolderName: "/app-folder"
//Създаваме pdf file ,с данните от LaborAgreementVM параметъра на метода Details ,  в Temp папката 

//|| x.EmploymentContract.ContractType.Type.Equals("Employment Contract")