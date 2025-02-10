using Payroll.Models;
using Payroll.ViewModels.EmployeeViewModels;
using Payroll.ViewModels.PersonViewModels;

namespace Payroll.Mapper.CustomMap
{
	public class ProfilePersonInfoContractVM
	{
		public IQueryable<PersonInfoContractVM>? Projection(IQueryable<Employee> employees)
		{
			IQueryable<PersonInfoContractVM>? personsCollection = employees
			.Select(x => new PersonInfoContractVM
			{
				EmployeeId = x.Id,
				CompanyId = x.CompanyId,
				PersonId = x.Person.Id,
				FullName = x.Person.FullName,
				GenderType = x.Person.Gender.Type,
				CivilNumber = x.Person.EGN,
				PermanentAddressId = x.Person.PermanentAddressId,
				PermanentAddress = new AddressEmpVM
				{
					Country = x.Person.PermanentAddress.Country,
					Region = x.Person.PermanentAddress.Region,
					Municipality = x.Person.PermanentAddress.Municipality,
					City = x.Person.PermanentAddress.City,
					Street = x.Person.PermanentAddress.Street,
					Number = x.Person.PermanentAddress.Number,
					Entrance = x.Person.PermanentAddress.Entrance,
					Floor = x.Person.PermanentAddress.Floor,
					ApartmentNumber = x.Person.PermanentAddress.ApartmentNumber,
				},
				DocumentNumber = x.Person.IdDocuments
										 .Where(y => y.DocumentType.DocumentName == "Identity Card"
												  && y.IsValid == true)
										 .FirstOrDefault()
										 .DocumentNumber,
				IdDocumentDateOfIssue = x.Person.IdDocuments
										 .Where(y => y.DocumentType.DocumentName == "Identity Card"
												  && y.IsValid == true)
										 .FirstOrDefault().DateOfIssue,
				IssuingAuthority = x.Person.IdDocuments
										 .Where(y => y.DocumentType.DocumentName == "Identity Card"
												  && y.IsValid == true)
										 .FirstOrDefault().IssuingAuthority,
				EducationName = x.Person.Diplomas
										.Where(x => x.HasBeenDeleted == false)
										.OrderBy(x => x.Id)
										.LastOrDefault().EducationType.Type,
				Speciality = x.Person.Diplomas
										.Where(x => x.HasBeenDeleted == false)
										.OrderBy(x => x.Id)
										.LastOrDefault().Speciality,
				DiplomaRegNumber = x.Person.Diplomas
										.Where(x => x.HasBeenDeleted == false)
										.OrderBy(x => x.Id)
										.LastOrDefault().DiplomaRegNumber,
				DiplomaDateOfIssue = x.Person.Diplomas
										.Where(x => x.HasBeenDeleted == false)
										.OrderBy(x => x.Id)
										.LastOrDefault().DateOfIssue,
				EducationalInstitution = "",
				WorkExperience = "",
				SpecialtyWorkExperience = "",

			});

			return personsCollection;
		}

	}
}

/*
 
 */