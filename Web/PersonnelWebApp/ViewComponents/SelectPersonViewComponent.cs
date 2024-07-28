using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels;
using PersonnelWebApp.Models;

namespace PersonnelWebApp.ViewComponents
{
    [ViewComponent(Name = "SelectPerson")]
	public class SelectPersonViewComponent : ViewComponent
	{
		private readonly IAllValidEntities<SearchPersonVM> service;

		public SelectPersonViewComponent(
			IAllValidEntities<SearchPersonVM> selectPersonService)
		{
			this.service = selectPersonService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var personList = new PersonListViewModel();

			var persons = await this.service.GetAllValidEntitiesAsync();

			foreach ( var item in persons )
			{
				personList.Persons.Add(new SelectListItem()
					{ 
						Text = item.ToString(),
						Value = item.Id.ToString()
					});
			}

			return View(personList);
		}
	}
}
