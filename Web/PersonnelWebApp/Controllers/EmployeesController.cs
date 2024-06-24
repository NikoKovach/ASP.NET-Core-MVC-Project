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
		IEmployee empService;
		private static int companyIdNumber = 0;

		public EmployeesController(IEmployee service)
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
				ContactInfo = new ContactsEmpDto(),
				IdCardPassport = new IdDocumentEmpDto(),
				ContractInfo = new ContractEmpDto(),
			};

			var items = new List<GetEmployeeDto>();
			items.Add( defaultItem );

			var defaultList = new PaginatedList<GetEmployeeDto>(items,1,1);

			return defaultList;
		}

	}
}
