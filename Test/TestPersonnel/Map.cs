using Payroll.Data;
using Payroll.Mapper.CustomMap;
using Payroll.Services.Services.EmployeeServices;
using Payroll.ViewModels.EmployeeViewModels;
using System.Linq;

namespace WebConsoleAppPersonnel
{
	public class Map
	{
		public static void MapTest(PayrollContext db,int companyId)
		{
			var empQueryCollection = db.Employees
				.Where( x => x.CompanyId == companyId )
				.Select( x => new GetEmployeeVM
				{
					Id = x.Id,
					CompanyId = companyId,
					NumberFromTheList = x.NumberFromTheList,
					IsPresent = x.IsPresent,
					Person = new PersonEmpVM
					{
						Id = x.Person.Id,
						FullName = x.Person.FirstName + " " +
						( x.Person.MiddleName ?? "" ) + " " + x.Person.LastName,
						GenderId = x.Person.GenderId,
						GenderType = x.Person.Gender.Type,
						EGN = x.Person.EGN,
						PhotoFilePath = x.Person.PhotoFilePath,
						PermanentAddress = new AddressEmpVM
						{
							Country = x.Person.PermanentAddress.Country,
							Region = x.Person.PermanentAddress.Region,
							Municipality = x.Person.PermanentAddress.Municipality,
							City = x.Person.PermanentAddress.City,
							Street = x.Person.PermanentAddress.Street,
							Number = x.Person.PermanentAddress.Number,
							Entrance = x.Person.PermanentAddress.Entrance,
						}.ToString()??"",
						CurrentAddress = new AddressEmpVM
						{
							Country = x.Person.CurrentAddress.Country,
							Region = x.Person.CurrentAddress.Region,
							Municipality = x.Person.CurrentAddress.Municipality,
							City = x.Person.CurrentAddress.City,
							Street = x.Person.CurrentAddress.Street,
							Number = x.Person.CurrentAddress.Number,
							Entrance = x.Person.CurrentAddress.Entrance,
						}.ToString()??"",
					},
					ContactInfo = new ContactsEmpVM
					{
						PhoneNumberOne = x.Person.ContactInfoList
									.OrderBy( x => x.Id )
									.LastOrDefault().PhoneNumberOne,
						PhoneNumberTwo = x.Person.ContactInfoList
									.OrderBy( x => x.Id )
									.LastOrDefault().PhoneNumberTwo,
						E_MailAddress1 = x.Person.ContactInfoList
									.OrderBy( x => x.Id )
									.LastOrDefault().E_MailAddress1,
						Website = x.Person.ContactInfoList
									.OrderBy( x => x.Id )
									.LastOrDefault().WebSite,
					},
					IdCardPassport = new IdDocumentEmpVM
					{
						DocumentName = x.Person.IdDocuments
										.OrderBy( x => x.Id )
										.LastOrDefault()
										.DocumentType
										.DocumentName,
						DocumentNumber = x.Person.IdDocuments
										.OrderBy( x => x.Id )
										.LastOrDefault()
										.DocumentNumber,
					},
					ContractInfo = new ContractEmpVM
					{
						JobTitle = x.EmploymentContract.JobTitle,
						DepartmentName = x.EmploymentContract.Department.Name,
						ContractType = x.EmploymentContract.ContractType.Type,
						ContractNumber = x.EmploymentContract.ContractNumber,
						ContractDate = x.EmploymentContract.ContractDate.Date,
					}
				} )
				.OrderBy( x => x.Person.FullName )
				.ToList();

			var realList = empQueryCollection.ToList();

			Console.WriteLine( $"{realList[ 0 ].Person.FullName} -> " +
				$"{realList[0].Person.EGN}" );
			//return empQueryCollection;

		}

		public static void MapTest2(PayrollContext db,int companyId)
		{
			var service = new GetEmployeeMapping();

			var result = service.MapAllEmployeesQueryable( db.Employees, companyId )
							.ToList();
			Console.WriteLine();
		}
	}
}
