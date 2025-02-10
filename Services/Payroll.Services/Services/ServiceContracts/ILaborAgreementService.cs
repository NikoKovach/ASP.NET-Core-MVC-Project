using Payroll.ViewModels.EmpContractViewModels;

namespace Payroll.Services.Services.ServiceContracts
{
	public interface ILaborAgreementService : IBasicAddUpdate<LaborAgreementVM>
	{
		IQueryable<LaborAgreementVM>? All(int? companyId);

		IQueryable<LaborAgreementVM>? AllActive(int? companyId);

		IQueryable<LaborAgreementVM>? AllActive(int? companyId, string? sortParam, FilterAgreementVM? filter);

		IQueryable<LaborAgreementVM>? GetContract(int? contractId, int? companyId);

		/// <summary>
		/// Return relative file path.
		/// </summary>
		/// <param name="appFolderPath"></param>
		/// <param name="relativeFolderName"></param>
		/// <returns></returns>
		Task<string?> CreateTempFileAsync(string appFolderPath, string? relativeFolderName,
										  int? companyId, int? agreementId, string? fileTypeVersion);
	}
}
