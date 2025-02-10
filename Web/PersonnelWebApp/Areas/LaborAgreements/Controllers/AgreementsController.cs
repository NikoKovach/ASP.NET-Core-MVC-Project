using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Payroll.Services.Services.ServiceContracts;
using Payroll.Services.UtilitiesServices.EntityValidateServices;
using Payroll.ViewModels;
using Payroll.ViewModels.EmpContractViewModels;
using Payroll.ViewModels.PagingViewModels;

using PersonnelWebApp.Utilities;

namespace PersonnelWebApp.Areas.Contracts.Controllers
{
	[Area("LaborAgreements")]
	public class AgreementsController : Controller
	{
		private readonly int _pageSize;
		private readonly int _pageIndex;
		private readonly int _count;

		private readonly ILaborAgreementService service;
		private readonly IValidate<ValidateBaseModel> validateService;
		private readonly IWebHostEnvironment env;
		private readonly IConfiguration config;

		public AgreementsController(
			   ILaborAgreementService agreementService,
			   IPrivateConfiguration configuration,
			   IConfiguration appConfiguration,
			   IWebHostEnvironment environment,
			   [FromKeyedServices("AgreementValidate")] IValidate<ValidateBaseModel> validateService)
		{
			this.service = agreementService;

			this.validateService = validateService;

			configuration.SetPagingVariables(ref this._pageIndex, ref this._pageSize, ref this._count);

			this.env = environment;

			this.config = appConfiguration;
		}

		[HttpPost]
		public async Task<IActionResult> Index(int? companyId, int? pageIndex, int? pageSize,
											   string? sortParam, FilterAgreementVM? filter)
		{
			if (!ModelState.IsValid)
				return await ResultAsync(companyId, pageIndex, pageSize, sortParam, new FilterAgreementVM());

			return await ResultAsync(companyId, pageIndex, pageSize, sortParam, filter);
		}

		[HttpPost]
		public IActionResult Create(LaborAgreementVM? agreementForEdit)
		{
			if (agreementForEdit.Id == null || agreementForEdit.Id < 1)
			{
				ModelState.Clear();
			}

			agreementForEdit.CompanyId = (agreementForEdit.CompanyId < 1) ? null : agreementForEdit.CompanyId;

			return View("CreateEditAgreement", agreementForEdit);
		}

		[HttpPost]
		public async Task<IActionResult> AddAgreement(LaborAgreementVM? agreementVM)
		{
			if (!ModelState.IsValid)
			{
				return View("CreateEditAgreement", agreementVM);
			}

			await this.service.AddAsync(agreementVM);

			return await ResultAsync(agreementVM.CompanyId, this._pageIndex, this._pageSize, default, default);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int? companyId, int? agreementId, string? viewTableRow)
		{
			LaborAgreementVM? agreementForEdit = await this.service
														   .GetContract(agreementId, companyId)
														   .FirstOrDefaultAsync();

			agreementForEdit.ViewTableRow = viewTableRow;

			this.validateService.Validate(ModelState, agreementForEdit);

			if (!ModelState.IsValid)
			{
				return await ResultAsync(companyId, this._pageIndex, this._pageSize, default, default);
			}

			return View("CreateEditAgreement", agreementForEdit);
		}

		[HttpPost]
		public async Task<IActionResult> EditAgreement(LaborAgreementVM? agreementVM)
		{
			if (!ModelState.IsValid)
			{
				return View("CreateEditAgreement", agreementVM);
			}

			await this.service.UpdateAsync(agreementVM);

			return await ResultAsync(agreementVM.CompanyId, this._pageIndex, this._pageSize, default, default);
		}

		[HttpPost]
		public async Task<IActionResult> Details(int? companyId, int? agreementId, string? fileTypeVersion)
		{
			//fileTypeVersion = bul-pdf ; eng-pdf ; bul-rtf ; eng-rtf
			string? appFolderPath = Path.Combine(this.env.ContentRootPath,
									this.config["PrimaryAppFolder:FolderName"]);

			string? relativeFolderName = this.config["PrimaryAppFolder:RequestPath"];

			string? contractFilePath =
						 await this.service.CreateTempFileAsync(appFolderPath, relativeFolderName,
																companyId, agreementId, fileTypeVersion);

			//return LocalRedirect( contractFilePath );

			return LocalRedirect("/app-folder/Temp/HelloWorldMigraDoc-WIN-CORE-6.0-5B93294859_temp.pdf");
			//"/app-folder/Temp/HelloWorldMigraDoc-WIN-CORE-6.0-5B93294859_temp.pdf" 
		}

		//*********************************************************

		private async Task<IActionResult> ResultAsync(int? companyId, int? pageIndex, int? pageSize,
													 string? sortParam, FilterAgreementVM? filter)
		{
			var sortedList = await GetAgreementListOfPagesAsync(companyId, pageIndex,
																pageSize, sortParam, filter);

			string? controllerName = this.RouteData.Values["controller"].ToString();

			sortedList.RouteEdit = $"/{controllerName}/{nameof(EditAgreement)}";

			return View("Index", sortedList);
		}

		private async Task<PagingListForContracts<LaborAgreementVM, FilterAgreementVM>>
			   GetAgreementListOfPagesAsync
			   (int? companyId, int? pageIndex, int? pageSize, string? sortParam, FilterAgreementVM? filter)
		{
			var sortedAgreementsList = this.service.AllActive(companyId, sortParam, filter ?? DefaultFilter());

			var sortedList = await PagingListForContracts<LaborAgreementVM, FilterAgreementVM>
							.CreateAgreementsCollectionAsync(sortedAgreementsList,
							pageIndex ?? this._pageIndex, pageSize ?? this._pageSize,
							sortParam, filter ?? DefaultFilter());


			sortedList.CompanyId = (companyId == null || companyId <= 0) ? -1 : companyId;

			if (sortedList.ItemsCollection.Count == 0)
				return EmptyAgreementsSortedCollection();

			return sortedList;
		}

		private PagingListForContracts<LaborAgreementVM, FilterAgreementVM> EmptyAgreementsSortedCollection()
		{
			List<LaborAgreementVM> defaultAgreementsList = new List<LaborAgreementVM>()
														   {
														   		new LaborAgreementVM()
														   };

			var emptyPaginatedList = new PagingListForContracts<LaborAgreementVM, FilterAgreementVM>
									(defaultAgreementsList, this._count, this._pageIndex,
									this._pageSize, string.Empty, DefaultFilter());

			return emptyPaginatedList;
		}

		private FilterAgreementVM DefaultFilter() => new FilterAgreementVM();
	}
}

//[HttpPost]
//public ActionResult Edit( LaborAgreementVM? agreementForEdit )
//{
//       if ( !ModelState.IsValid )
//       {
//              return View( "CreateEditAgreement", agreementForEdit );
//       }

//       return View( "CreateEditAgreement", agreementForEdit );
//}

//if ( !string.IsNullOrEmpty( viewTableRow ) )
//{
//       return Json( ModelState );
//}

//[HttpPost]
//public ActionResult Delete( int? companyId, int? employeeId )
//{
//       try
//       {
//              return RedirectToAction( nameof( Index ) );
//       }
//       catch
//       {
//              return View();
//       }
//}