using LegalFramework.Services.CalculationOfWorkExperience;

using Microsoft.AspNetCore.Mvc;

using Payroll.ViewModels.EmployeeViewModels;

namespace PersonnelWebApp.ViewComponents
{
    [ViewComponent( Name = "WorkExperience" )]
	public class WorkExperienceViewComponent : ViewComponent
	{
		private readonly ICalculateExperience service;

		public WorkExperienceViewComponent(
			ICalculateExperience calculateService )
		{
			this.service = calculateService;
		}

		public async Task<IViewComponentResult> InvokeAsync( int? companyId, int empId )
		{
			WorkExperienceVM resultExperience = new WorkExperienceVM();

			//if ( companyId == null || companyId == 0 || companyId == 0)
			//{
			//	return View(resultExperience);
			//}

			resultExperience = await this.service.CalculateAsync( companyId, empId );

			return View( resultExperience );
		}
	}
}
