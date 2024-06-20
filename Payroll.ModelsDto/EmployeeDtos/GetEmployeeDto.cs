
using System.ComponentModel.DataAnnotations;

namespace Payroll.ModelsDto.EmployeeDtos
{
	public class GetEmployeeDto
	{
		public int Id { get; set; }

		[Display(Name = "Company")]
		public int? CompanyId { get; set; }

		[Display(Name = "Number From The List")]
		public string? NumberFromTheList { get; set; }

		[Display(Name = "Is Active")]
          public bool IsPresent { get; set; }

		public PersonEmpDto? PersonDto { get; set; }

		public ContactsEmpDto? ContactInfo { get; set; }

		public IdDocumentEmpDto? IdCardPassport { get; set; }

		//*******************************************************
		public ContractEmpDto? ContractInfo { get; set; }
		
		[Display(Name = "Permanent Address")]
		public string? PermanentAddress { get; set; }
		//Calculate Field - <vc:VCName companyId="" empId=""></vc:VCName>

		[Display(Name = "Current Address")]
		public string? CurrentAddress { get; set; }
		//Calculate Field - <vc:VCName companyId="" empId=""></vc:VCName>

		[Display(Name = "Work Experience")]
		public string? WorkExperience { get; set; } 
		//Calculate Field - <vc:VCName companyId="" empId=""></vc:VCName>

		[Display(Name = "Specialty Work Experience")]
          public string? SpecialtyWorkExperience  { get; set; } 
		//Calculate Field - <vc:VCName companyId="" empId=""></vc:VCName>
	}
}
