//using Microsoft.AspNetCore.Mvc;
//using Payroll.Services.Services.ServiceContracts;
//using Payroll.ViewModels.EmployeeViewModels;

//namespace PersonnelWebApp.ViewComponents
//{
//    [ViewComponent(Name = "ContractInfo")]
//	public class ContractInfoViewComponent : ViewComponent
//	{
//		private readonly IGetContractInfo service;

//		public ContractInfoViewComponent(
//			IGetContractInfo service)
//		{
//			this.service = service;
//		}

//		public async Task<IViewComponentResult> InvokeAsync(int? companyId,int empId)
//		{
//			ContractEmpVM resultDto = new ContractEmpVM();

//			if ( companyId == null || companyId == 0)
//			{
//				return View(resultDto);
//			}

//			resultDto = await this.service.GetContractAsync(companyId,empId);

//			return View(resultDto);
//		}
//	}
//}
