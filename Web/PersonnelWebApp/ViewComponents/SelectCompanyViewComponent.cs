using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels;
using PersonnelWebApp.Models;

namespace PersonnelWebApp.ViewComponents
{
    [ViewComponent(Name = "SelectCompany")]
	public class SelectCompanyViewComponent : ViewComponent
	{
		private readonly IAllValidEntities<SearchCompanyViewModel> service;

		public SelectCompanyViewComponent(
			IAllValidEntities<SearchCompanyViewModel> selectCompanyService)
		{
			this.service = selectCompanyService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var companyList = new CompanyListViewModel();

			var companies = await this.service.GetAllValidEntitiesAsync();

			foreach ( var item in companies )
			{
				companyList.Companies.Add(new SelectListItem() 
				{ 
					Text = $"{item.Info}",
					Value = item.Id.ToString()
				} 
				);
			}

			return View(companyList);

		}

	}
}
