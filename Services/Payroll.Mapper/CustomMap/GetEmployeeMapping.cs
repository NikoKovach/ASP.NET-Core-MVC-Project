using Payroll.Data;
using Payroll.ModelsDto.EmployeeDtos;

namespace Payroll.Mapper.CustomMap
{
	public class GetEmployeeMapping : IGetEmployeeMapping
	{
		public IQueryable<GetEmployeeDto> MapAllEmployeesQueryable( PayrollContext db, int companyId )
		{
			var empQueryCollection = db.Employees
				.Where( x => x.CompanyId == companyId)
				.Select( x => new GetEmployeeDto
				{
					Id = x.Id,
					CompanyId = companyId,
					NumberFromTheList = x.NumberFromTheList,
					IsPresent = x.IsPresent,
					PersonDto = new PersonEmpDto
					{
						Id = x.Person.Id,
						FirstName = x.Person.FirstName,
						MiddleName = x.Person.MiddleName,
						LastName = x.Person.LastName,
						GenderId = x.Person.GenderId,
						GenderType = x.Person.Gender.Type,
						EGN = x.Person.EGN,
						PhotoFilePath = x.Person.PhotoFilePath,
					},
					ContactInfo = new ContactsEmpDto
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
						WebSite = x.Person.ContactInfoList
									.OrderBy( x => x.Id )
									.LastOrDefault().WebSite,
					},
					IdCardPassport = new IdDocumentEmpDto
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
					ContractInfo = new ContractEmpDto
					{
						JobTitle = x.EmploymentContract.JobTitle,
						DepartmentName = x.EmploymentContract.Department.Name,
						ContractType = x.EmploymentContract.ContractType.Type,
						ContractNumber = x.EmploymentContract.ContractNumber,
						ContractDate = x.EmploymentContract.ContractDate,
					}
				} )
				.OrderBy( x => x.PersonDto.FirstName )
				.ThenBy( x => x.PersonDto.LastName );

			return empQueryCollection;
		}

		public IQueryable<GetEmployeeDto> MapPresentEmployeesQueryable
			( PayrollContext db, int companyId )
		{
			var empQueryCollection = MapAllEmployeesQueryable( db, companyId )
				.Where( x => x.IsPresent == true );

			return empQueryCollection;
		}
	}
}