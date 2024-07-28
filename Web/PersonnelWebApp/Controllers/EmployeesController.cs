using Microsoft.AspNetCore.Mvc;
using Payroll.Services.Services.EmployeeServices;
using Payroll.Services.Services.ServiceContracts;
using Payroll.ViewModels.EmployeeViewModels;
using PersonnelWebApp.Models;

namespace PersonnelWebApp.Controllers
{
	public class EmployeesController : Controller
	{
		private readonly IEmployee empService;
		private readonly IGetContractInfo contractService;

		private static int companyIdNumber = 0;

		public EmployeesController(IEmployee service,IGetContractInfo contractService)
		{
			this.empService = service;

			this.contractService = contractService;
		}

		public IActionResult Index()
		{
			var emptyPaginatedList = new PaginatedCollection<GetEmployeeVM>( EmptyEmpoyeesCollection(), 1, 1 );

			return View(emptyPaginatedList);
		}

		[HttpPost]
		public async Task<IActionResult> Index( int companyId, int? pageNumber)
		{
			if ( companyId != 0 )
			{
				companyIdNumber = companyId;
			}

			var empList = this.empService
						   .AllPresentEmployees(companyIdNumber);

			var paginatedList = await PaginatedCollection<GetEmployeeVM>
							.CreateCollectionAsync( empList,pageNumber ?? 1) ;

			await GetContractInfoAsync( companyId, paginatedList.ItemsCollection);

			if ( paginatedList.ItemsCollection.Count == 0 )
			{
				var emptyPaginatedList = new PaginatedCollection<GetEmployeeVM>
					( EmptyEmpoyeesCollection(), 1, 1 );

				return Json(emptyPaginatedList);
			}

			return Json(paginatedList);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(EmployeeVM empViewModel)
		{

			if ( !ModelState.IsValid )
			{
				return View(nameof(Create));
			}

			//await this.empService.AddAsync( empViewModel );

			//Implement empService.AddAsync method
			//Създаване на папка за служител
			//Копиране снимка на човек на сървъра.

			var demo = new Demo() 
			{ 
				Text = "Create"
			};

			return View("EditDemo",demo);
		}

		[HttpPost]
		public IActionResult Edit(EmployeeVM empViewModel)
		{
			Console.WriteLine("Edit");
			if ( !ModelState.IsValid )
			{
				return View(nameof(Create));
			}

			//await this.empService.AddAsync( empViewModel );

			//Implement empService.AddAsync method
			//Създаване на папка за служител
			//Копиране снимка на човек на сървъра.

			var demo = new Demo() 
			{ 
				Text =  "Edit"
			};

			return View("EditDemo",demo);
		}
//*******************************************************************

		private List<GetEmployeeVM> EmptyEmpoyeesCollection()
		{
			GetEmployeeVM defaultItem = new GetEmployeeVM()
			{
				Person = new PersonEmpVM(),
				ContactInfo = new ContactsEmpVM(),
				IdCardPassport = new IdDocumentEmpVM(),
				ContractInfo = new ContractEmpVM(),
			};

			var items = new List<GetEmployeeVM>
			{
				defaultItem
			};

			return items;
		}

		private async Task GetContractInfoAsync( int? companyId, List<GetEmployeeVM>empCollection )
		{
			foreach ( var item in empCollection )
			{
				int empId = item.Id;
				ContractEmpVM resultDto = new ContractEmpVM();

				if ( companyId == null || companyId == 0)
				{
					item.ContractInfo = default;
				}
				else
				{
					resultDto = await this.contractService
						.GetContractAsync( companyId, empId );

					item.ContractInfo = resultDto;
				}
			}
		}
	}
}