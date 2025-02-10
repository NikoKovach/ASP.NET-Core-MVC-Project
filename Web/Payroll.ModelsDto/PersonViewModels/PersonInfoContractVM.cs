using Payroll.ViewModels.EmployeeViewModels;

namespace Payroll.ViewModels.PersonViewModels
{
	public class PersonInfoContractVM : ValidateBaseModel
	{
		public int? EmployeeId { get; set; }

		public int? CompanyId { get; set; }

		public int? PersonId { get; set; }

		public string? FullName { get; set; }

		public string? GenderType { get; set; }

		public string? CivilNumber { get; set; }

		public int? PermanentAddressId { get; set; }

		public AddressEmpVM PermanentAddress { get; set; }

		public string? DocumentNumber { get; set; }

		public DateTime? IdDocumentDateOfIssue { get; set; }

		public string? IssuingAuthority { get; set; }

		public string? EducationName { get; set; }

		public string? Speciality { get; set; }

		public string? DiplomaRegNumber { get; set; }

		public DateTime? DiplomaDateOfIssue { get; set; }

		public string? EducationalInstitution { get; set; }

		public string? WorkExperience { get; set; }

		public string? SpecialtyWorkExperience { get; set; } // lenght- 6 characters - 050125 (05 г.01 м.25 д.)
	}
}

//public string? FirstName { get; set; }

//public string? MiddleName { get; set; }

//public string? LastName { get; set; }