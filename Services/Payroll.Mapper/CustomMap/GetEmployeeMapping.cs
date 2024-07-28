using Microsoft.EntityFrameworkCore;
using Payroll.Models;
using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.Mapper.CustomMap
{
	public class GetEmployeeMapping : IGetEmployeeMapping
	{
		public IQueryable<GetEmployeeVM> MapAllEmployeesQueryable
			( DbSet<Employee> employees, int companyId )
		{
			var empQueryCollection = employees
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
						FullName = (x.Person.FirstName + " " +
						( x.Person.MiddleName ?? "" ) + " " + x.Person.LastName)??"",
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
					ContractInfo = new ContractEmpVM()
				} )
				.OrderBy( x => x.Person.FullName );

			return empQueryCollection;
		}

		public IQueryable<GetEmployeeVM> MapPresentEmployeesQueryable
			( DbSet<Employee> employees, int companyId )
		{
			var empQueryCollection = MapAllEmployeesQueryable( employees, companyId )
				.Where( x => x.IsPresent == true );

			//var finalresult = empQueryCollection.ToList();

			return empQueryCollection;
		}

	}
}