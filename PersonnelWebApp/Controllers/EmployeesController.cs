using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.ModelsDto.EmployeeDtos;
using Payroll.Services.Services.EmployeeServices;
using Payroll.Services.Services.ServiceContracts;

namespace PersonnelWebApp.Controllers
{
	public class EmployeesController : Controller
	{
		IEmployeeService empService;
		private static int companyIdNumber = 0;

		public EmployeesController(IEmployeeService service)
		{
			this.empService = service;

		}

		public async Task<IActionResult> Index( int companyId, int? pageNumber)
		{
			if ( companyId != 0 )
			{
				companyIdNumber = companyId;
			}

			var empList = this.empService
						   .AllPresentEmployees(companyIdNumber);

			var paginatedList = await PaginatedList<GetEmployeeDto>
							.CreateAsync( empList,pageNumber ?? 1) ;

			if ( paginatedList.Count == 0 )
			{
				PaginatedList<GetEmployeeDto> emptyPaginatedList = new PaginatedList<GetEmployeeDto>( EmptyEmpCollection(), 1, 1 );

				return View(emptyPaginatedList);
			}

			return View(paginatedList);
		}

		//[HttpPost]
		public IActionResult AllPresentEmployees()
		{
			return View();
		}

		private PaginatedList<GetEmployeeDto> EmptyEmpCollection()
		{
			GetEmployeeDto defaultItem = new GetEmployeeDto() 
			{ 
				Id = -1,
				PersonDto = new PersonEmpDto 
				{ 
					FirstName = "No Record",
					LastName = "No REcord"
				},
			};
			var items = new List<GetEmployeeDto>();
			items.Add( defaultItem );

			var defaultList = new PaginatedList<GetEmployeeDto>(items,1,1);

			return defaultList;
		}

		// GET: EmployeesController/Details/5
		//public ActionResult Details( int id )
		//{
		//	return View();
		//}

		//// GET: EmployeesController/Create
		//public ActionResult Create()
		//{
		//	return View();
		//}

		//// POST: EmployeesController/Create
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Create( IFormCollection collection )
		//{
		//	try
		//	{
		//		return RedirectToAction( nameof( Index ) );
		//	}
		//	catch
		//	{
		//		return View();
		//	}
		//}

		//// GET: EmployeesController/Edit/5
		//public ActionResult Edit( int id )
		//{
		//	return View();
		//}

		//// POST: EmployeesController/Edit/5
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Edit( int id, IFormCollection collection )
		//{
		//	try
		//	{
		//		return RedirectToAction( nameof( Index ) );
		//	}
		//	catch
		//	{
		//		return View();
		//	}
		//}

		//// GET: EmployeesController/Delete/5
		//public ActionResult Delete( int id )
		//{
		//	return View();
		//}

		//// POST: EmployeesController/Delete/5
		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public ActionResult Delete( int id, IFormCollection collection )
		//{
		//	try
		//	{
		//		return RedirectToAction( nameof( Index ) );
		//	}
		//	catch
		//	{
		//		return View();
		//	}
		//}
	}
}
