﻿using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.PagingViewModels;
using Payroll.ViewModels.PersonViewModels;

using PersonnelWebApp.Utilities;

namespace PersonnelWebApp.Controllers
{
	public class DiplomasController : Controller
	{
		private int _pageSize;
		private int _pageIndex;
		private int _count;

		private readonly IDiplomaService service;
		private IConfigurationRoot? privateConfig;

		public DiplomasController(IDiplomaService diplomaService, IPrivateConfiguration configuration)
		{
			this.service = diplomaService;

			this.privateConfig = configuration.PrivateConfig();

			SetPagingVariables();
		}

		[HttpPost]
		public async Task<IActionResult> Index(
			   [Required(ErrorMessage ="Select person from person's table ! Go back to 'Persons List' .")]
					 int? personId,
			   int? pageIndex, int? pageSize, string? sortParam)
		{
			if (!ModelState.IsValid)
			{
				return await ResultAsync(personId, pageIndex, pageSize, sortParam);
			}

			return await ResultAsync(personId, pageIndex, pageSize, sortParam);
		}

		[HttpPost]
		public async Task<IActionResult> Create(int? personId, int? pageIndex, int? pageSize,
			   string? sortParam, DiplomaVM? diplomaVM)
		{
			if (!ModelState.IsValid)
			{
				return await ResultAsync(personId, pageIndex, pageSize, sortParam);
			}

			await this.service.AddAsync(diplomaVM);

			return await ResultAsync(personId, pageIndex, pageSize, sortParam);
		}


		[HttpPost]
		public async Task<IActionResult> Edit([Required] int? personId, int? pageIndex, int? pageSize,
												  string? sortParam, List<DiplomaVM>? entitiesForEdit)
		{
			if (!ModelState.IsValid)
			{
				if (entitiesForEdit.Count > 0)
				{
					return Json(ModelState);
				}

				return await ResultAsync(personId, pageIndex, pageSize, sortParam);
			}

			await this.service.UpdateAsync(entitiesForEdit);

			return Json(ModelState);
		}

		[HttpPost]
		public async Task<IActionResult> Delete([Required] int? personId, [Required] int? diplomaId,
												  int? pageIndex, int? pageSize, string? sortParam)
		{
			if (!ModelState.IsValid)
			{
				return await ResultAsync(personId, pageIndex, pageSize, sortParam);
			}

			await this.service.DeleteAsync(diplomaId, personId);

			return await ResultAsync(personId, pageIndex, pageSize, sortParam);
		}

		//##############################################################

		private async Task<IActionResult> ResultAsync(int? personId, int? pageIndex, int? pageSize,
																				string? sortParam)
		{
			PagingListSorted<DiplomaVM>? pagingSortedList =
					await GetDiplomasListOfPagesAsync(personId, pageIndex, pageSize, sortParam);

			string? controllerName = this.RouteData.Values["controller"].ToString();

			pagingSortedList.RouteEdit = $"/{controllerName}/{nameof(Edit)}";

			return View("Index", pagingSortedList);
		}

		private async Task<PagingListSorted<DiplomaVM>>? GetDiplomasListOfPagesAsync
			   (int? personId, int? pageIndex, int? pageSize, string? sortParam)
		{
			IQueryable<DiplomaVM>? sortedDiplomasList = this.service.AllNotDeleted(personId, sortParam);

			if (sortedDiplomasList is null)
				return EmptyDiplomasSortedCollection(personId);

			PagingListSorted<DiplomaVM>? sortedList =
				   await PagingListSorted<DiplomaVM>.CreateSortedCollectionAsync
					  (sortedDiplomasList, pageIndex ?? this._pageIndex, pageSize ?? this._pageSize, sortParam);

			if (sortedList.ItemsCollection.Count == 0)
				return EmptyDiplomasSortedCollection(personId);

			return sortedList;
		}

		private PagingListSorted<DiplomaVM> EmptyDiplomasSortedCollection(int? personId)
		{
			List<DiplomaVM> defaultDiplomasList = new List<DiplomaVM>()
					 {
							new DiplomaVM{PersonId = personId,}
					 };

			PagingListSorted<DiplomaVM> emptyPaginatedList = new PagingListSorted<DiplomaVM>
				   (defaultDiplomasList, this._count, this._pageIndex, this._pageSize, string.Empty);

			return emptyPaginatedList;
		}

		private void SetPagingVariables()
		{
			if (!string.IsNullOrEmpty(this.privateConfig["Paging:PageSize"]))
			{
				this._pageSize = int.Parse(this.privateConfig["Paging:PageSize"]);
			}

			if (!string.IsNullOrEmpty(this.privateConfig["Paging:PageIndex"]))
			{
				this._pageIndex = int.Parse(this.privateConfig["Paging:PageIndex"]);
			}

			if (!string.IsNullOrEmpty(this.privateConfig["Paging:Count"]))
			{
				this._count = int.Parse(this.privateConfig["Paging:Count"]);
			}
		}
	}
}
